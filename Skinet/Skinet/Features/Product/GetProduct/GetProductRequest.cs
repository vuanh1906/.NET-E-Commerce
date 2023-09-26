using MediatR;
using System.Security.AccessControl;

namespace Skinet.Features.Product.GetProduct
{
    public class GetProductRequest : IRequest<GetProductResponse>
    {
        public int ProductId { get; set; }
    }
}