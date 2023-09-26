using Core.Entities.Dtos;
using Core.Interfaces;
using MediatR;
using Skinet.Dtos;

namespace Skinet.Features.Product.GetProducts
{
    public class GetProductsHandler : IRequestHandler<GetProductsRequest, GetProductsResponse>
    {
        private readonly IProductService _productService;

        public GetProductsHandler(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<GetProductsResponse> Handle(GetProductsRequest request, CancellationToken cancellationToken)
        {
            var filter = new ProductFilterDto
            {
                Brands = request.Brands,
                Types = request.Types,
                PageIndex = request.PageIndex,  
                PageSize = request.PageSize,    
                Search = request.Search,
                Sort = request.Sort,
            };
           

            var products = _productService.GetProductsWithFilters(filter);
            var total = await _productService.Total();
            return new GetProductsResponse
            {
                Count = total,
                PageIndex = (int)filter.PageIndex,
                PageSize = (int)filter.PageSize,
                TotalPage = (int)Math.Ceiling(total / (double)filter.PageSize)
            };
        }
    }
}
