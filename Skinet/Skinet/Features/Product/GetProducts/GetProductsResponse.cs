namespace Skinet.Features.Product.GetProducts
{
    public class GetProductsResponse
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int Count { get; set; }
        public int TotalPage { get; set; }
        public IReadOnlyList<Core.Entities.Product> Data { get; set; }
    }
}
