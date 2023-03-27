using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Skinet.Dtos;
using Skinet.Errors;

namespace Skinet.Controllers
{
    public class BasketController : BaseApiController
    {
        private readonly IGenericRepository<Product> _productRepo;
        private readonly IBasketRepository _basketRepo;
        private StoreContext _context { get; set; }
        public BasketController(IGenericRepository<Product> productRepo, StoreContext context, IBasketRepository basketRepo)
        {
            _productRepo = productRepo;
            _context = context;
            _basketRepo = basketRepo;
        }

        [HttpGet(Name = "GetBasket")]
        public async Task<ActionResult<BasketDto>> GetBasket()
        {
            var basket = await RetrieveBasket();
            if (basket == null) return NotFound();
            return MapBasketToDto(basket);
        }

        [HttpPost]  // api/basket?productId=3&quantity=2
        public async Task<ActionResult<BasketDto>> AddItemToBasket(int productId, int quantity)
        {
            var basket = await RetrieveBasket();
            if (basket == null) basket = CreateBasket();
            var spec = new ProductsWithTypesAndBrandsSpecification(productId);
            var product = await _productRepo.GetEntityWithSpec(spec);

            if (product == null) return NotFound();

            _basketRepo.AddItem(basket, product, quantity);
            var result = await _basketRepo.SaveAsync() > 0;
            if (result) return CreatedAtRoute("GetBasket", MapBasketToDto(basket));
            return BadRequest(new ApiResponse(400, "Problem saving item to basket"));
        }

        [HttpDelete]
        public async Task<ActionResult> RemoveBasketItem(int productId, int quantity)
        {
            var basket = await RetrieveBasket();
            if (basket == null) return NotFound();

            _basketRepo.RemoveItem(basket, productId, quantity);
            var result = await _basketRepo.SaveAsync() > 0;

            if (result) return StatusCode(201);
            return BadRequest(new ApiResponse(400, "Problem removing item from the basket"));
        }

        private async Task<Basket> RetrieveBasket()
        {
            var buyerId = Request.Cookies["buyerId"];
            var basket = await _context.Baskets
                .Include(i => i.Items)
                    .ThenInclude(p => p.Product)
                        .ThenInclude(b => b.ProductBrand)
                .Include(i => i.Items)
                    .ThenInclude(p => p.Product)
                        .ThenInclude(b => b.ProductType)
                .FirstOrDefaultAsync(b => b.BuyerId == Request.Cookies["buyerId"]);
            return basket;
        }

        private Basket CreateBasket()
        {
            var buyerId = Guid.NewGuid().ToString();
            var cookieOptions = new CookieOptions
            {
                HttpOnly = false,
                IsEssential = true,
                Expires = DateTime.Now.AddDays(30),
            };
            Response.Cookies.Append("buyerId", buyerId, cookieOptions);
            var basket = new Basket { BuyerId = buyerId };
            _context.Baskets.Add(basket);
            return basket;
        }

        private BasketDto MapBasketToDto(Basket basket)
        {
            return new BasketDto
            {
                Id = basket.Id,
                BuyerId = basket.BuyerId,
                Items = basket.Items.Select(item => new BasketItemDto
                {
                    ProductId = item.ProductId,
                    Name = item.Product.Name,
                    Price = item.Product.Price,
                    PictureUrl = item.Product.PictureUrl,
                    Type = item.Product.ProductType.Name,
                    Brand = item.Product.ProductBrand.Name,
                    Quantity = item.Quantity
                }).ToList()
            };
        }
    }
}
