using ECommerce.Domain.Entities.ProductModule;
using ECommerce.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Services.Specifications.ProductSpecifications
{
    internal class ProductWithTypeAndBrandSpecification:BaseSpecifications<Product,int>
    {
        public ProductWithTypeAndBrandSpecification(ProductQueryParams queryParams) 
            :base(p=>((!queryParams.brandId.HasValue||p.ProductBrandId== queryParams.brandId.Value)
            &&(!queryParams.typeId.HasValue||p.ProductTypeId== queryParams.typeId.Value))
            &&((string.IsNullOrEmpty(queryParams.search))||p.Name.ToLower().Contains(queryParams.search.ToLower()))
            )
        {
            AddInclude(p => p.ProductBrand);
            AddInclude(p => p.ProductType);
        }
        public ProductWithTypeAndBrandSpecification(int id):base(p=>p.Id==id) 
        {
            AddInclude(p => p.ProductBrand);
            AddInclude(p => p.ProductType);
        }
        
    }
}
