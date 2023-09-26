using Core.Entities;
using Core.Entities.Dtos;

namespace Core.Interfaces
{
    public interface IProductService
    {
        Task<IReadOnlyList<Product>> GetProductsWithFilters(ProductFilterDto filterDto);
        Task<Product> GetProductByIdAsync(int id);
        Task<IReadOnlyList<ProductBrand>> GetProductBrandsAsync();
        Task<IReadOnlyList<ProductType>> GetProductTypesAsync();
        Task<int> Total();
    }
}
