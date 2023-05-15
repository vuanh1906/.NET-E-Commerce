using AutoMapper;
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
        private StoreContext _context;
        private readonly IMapper _mapper;
        public BasketController(IGenericRepository<Product> productRepo, StoreContext context, IBasketRepository basketRepo, IMapper mapper)
        {
            _productRepo = productRepo;
            _context = context;
            _basketRepo = basketRepo;
            _mapper = mapper;
        }

        [HttpGet(Name = "GetBasket")]
        public async Task<ActionResult<BasketDto>> GetBasket()
        {
            var basket = await RetrieveBasket(GetBuyerId());
            if (basket == null) return NotFound();
            return _mapper.Map<BasketDto>(basket);
        }

        [HttpPost]  // api/basket?productId=3&quantity=2
        public async Task<ActionResult<BasketDto>> AddItemToBasket(int productId, int quantity)
        {
            var basket = await RetrieveBasket(GetBuyerId());
            if (basket == null) basket = CreateBasket();
            var spec = new ProductsWithTypesAndBrandsSpecification(productId);
            var product = await _productRepo.GetEntityWithSpec(spec);

            if (product == null) return BadRequest(new ApiResponse(400, "Product not found"));

            _basketRepo.AddItem(basket, product, quantity);
            var result = await _basketRepo.SaveAsync() > 0;
            if (result) return CreatedAtRoute("GetBasket", _mapper.Map<BasketDto>(basket));
            return BadRequest(new ApiResponse(400, "Problem saving item to basket"));
        }

        [HttpDelete]
        public async Task<ActionResult> RemoveBasketItem(int productId, int quantity)
        {
            var basket = await RetrieveBasket(GetBuyerId());
            if (basket == null) return NotFound();

            _basketRepo.RemoveItem(basket, productId, quantity);
            var result = await _basketRepo.SaveAsync() > 0;

            if (result) return StatusCode(201);
            return BadRequest(new ApiResponse(400, "Problem removing item from the basket"));
        }

        private async Task<Basket> RetrieveBasket(string buyerId)
        {
            if (string.IsNullOrEmpty(buyerId))
            {
                Response.Cookies.Delete("buyerId");
                return null;
            }
            var basket = await _context.Baskets
                .Include(i => i.Items)
                    .ThenInclude(p => p.Product)
                        .ThenInclude(b => b.ProductBrand)
                .Include(i => i.Items)
                    .ThenInclude(p => p.Product)
                        .ThenInclude(b => b.ProductType)
                .FirstOrDefaultAsync(b => b.BuyerId == buyerId);
            return basket;
        }

        private string GetBuyerId()
        {
            return User.Identity?.Name ?? Request.Cookies["buyerId"];
        }

        private Basket CreateBasket()
        {
            var buyerId = User.Identity?.Name;
            if (string.IsNullOrEmpty(buyerId))
            {
                buyerId = Guid.NewGuid().ToString();
                var cookieOptions = new CookieOptions
                {
                    HttpOnly = false,
                    IsEssential = true,
                    Expires = DateTime.Now.AddDays(30),
                };
                Response.Cookies.Append("buyerId", buyerId, cookieOptions);
            }
            var basket = new Basket { BuyerId = buyerId };
            _context.Baskets.Add(basket);
            return basket;
        }

        
    }
}
