using Core.Entities;
using Core.Entities.Dtos;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class ProductService : IProductService
    {
        public readonly StoreContext _context;

        public ProductService(StoreContext context)
        {
            _context = context;
        }

        public async Task<IReadOnlyList<ProductBrand>> GetProductBrandsAsync()
        {
            var brands = await _context.ProductBrands.ToListAsync();

            return brands;
        }

        public async Task<IReadOnlyList<ProductType>> GetProductTypesAsync()
        {
            var types = await _context.ProductTypes.ToListAsync();

            return types;
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _context.Products
                .Include(p => p.ProductType)
                .Include(p => p.ProductBrand)
                .SingleAsync(p => p.Id == id);
        }

        public async Task<IReadOnlyList<Product>> GetProductsWithFilters(ProductFilterDto filterDto)
        {
            var products = await _context.Products
                            .Where(p =>
                                (string.IsNullOrEmpty(filterDto.Search) || p.Name.ToLower().Contains(filterDto.Search.ToLower())) &&
                                (string.IsNullOrEmpty(filterDto.Brands) || p.ProductBrand.Name.ToLower() == filterDto.Brands.ToLower()) &&
                                (string.IsNullOrEmpty(filterDto.Types) || p.ProductType.Name.ToLower() == filterDto.Types.ToLower()))
                            .OrderBy(p => filterDto.Sort == "priceAsc" ? p.Price : filterDto.Sort == "priceDesc" ? -p.Price : 0)
                            .Include(p => p.ProductType).Include(p => p.ProductBrand)
                            .Skip(((int)filterDto.PageIndex - 1) * (int)filterDto.PageSize).Take((int)filterDto.PageSize).ToListAsync();
            return products;
        }

        public async Task<int> Total()
        {
            return await _context.Products.CountAsync();
        }
    }
}
