using ECommerce.Domain.Entities.ProductModule;
using ECommerce.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Services.Specifications.ProductSpecifications
{
    internal class ProductWithCountSpecification:BaseSpecifications<Product,int>
    {
        public ProductWithCountSpecification(ProductQueryParams queryParams) : base(ProductSpecificationsHelper.GetCriteria(queryParams))
        {
            
        }
    }
}
