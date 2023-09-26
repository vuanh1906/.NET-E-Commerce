using Core.Entities.Dtos;
using Core.Interfaces;
using MediatR;
using Skinet.Dtos;
using Skinet.Features.Product.GetProduct;

namespace Skinet.Features.Product.GetProduct
{
    public class GetProductHandler : IRequestHandler<GetProductRequest, GetProductResponse>
    {
        private readonly IProductService _productService;

        public GetProductHandler(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<GetProductResponse> Handle(GetProductRequest request, CancellationToken cancellationToken)
        {
            var product = await _productService.GetProductByIdAsync(request.ProductId);

            return new GetProductResponse
            {
                Product = product,
            };
        }
    }
}
