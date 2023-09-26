using AutoMapper;
using Core.Api.Infrastructure;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Skinet.Controllers;
using Skinet.Dtos;
using Skinet.Features.Product.GetProduct;
using Skinet.Features.Product.GetProducts;


namespace Skinet.Features.Product
{
    public class ProductController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly ILogger<ProductController> _logger;
        public ProductController(IMediator mediator,
                                 ILogger<ProductController> logger)
        {
           _mediator = mediator;
           _logger = logger;
        }

        [HttpPost]
        public async Task<ActionResult> GetProducts([FromBody] GetProductsRequest request)
        {
            _logger.LogInformation($"Get Products request. UserId = {UserId}");

            await _mediator.Send(request);

            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetProduct([FromBody] GetProductRequest request)
        {
            _logger.LogInformation($"Get Products request. UserId = {UserId}");

            await _mediator.Send(request);

            return Ok();
        }

        [HttpGet("brands")]
        public async Task<ActionResult<List<Product>>> GetProductBrands()
        {
            var productBrands = await _productBrandRepo.ListAllAsync();
            return Ok(productBrands);
        }

        [HttpGet("types")]
        public async Task<ActionResult<List<Product>>> GetProductTypes()
        {
            var productTypes = await _productTypeRepo.ListAllAsync();
            return Ok(productTypes);
        }

        [HttpGet("filters")]
        public async Task<ActionResult<List<Product>>> GetFilters()
        {
            var productBrands = await _productBrandRepo.ListAllAsync();
            var brands = productBrands.Select(x => x.Name).Distinct().ToList();
            var productTypes = await _productTypeRepo.ListAllAsync();
            var types = productTypes.Select(x => x.Name).Distinct().ToList();
            return Ok(new { brands, types });
        }
    }

}
