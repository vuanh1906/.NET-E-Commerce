using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Specifications
{
    public class ProductWithFiltersForCountSpecification : BaseSpecification<Product>
    {
        public ProductWithFiltersForCountSpecification(ProductSpecParams productParams) 
            : base (x =>
                (string.IsNullOrEmpty(productParams.Search) || x.Name.ToLower().Contains((productParams.Search))) &&
                (string.IsNullOrEmpty(productParams.Brands) || x.ProductBrand.Name == productParams.Brands) &&
                (string.IsNullOrEmpty(productParams.Types) || x.ProductType.Name == productParams.Types)
            )
        {

        }
    }
}
