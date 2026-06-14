using ECommerce.Domain.Entities.ProductModule;
using ECommerce.Shared;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace ECommerce.Services.Specifications.ProductSpecifications
{
    internal static class ProductSpecificationsHelper
    {
        public static Expression<Func<Product,bool>>GetCriteria(ProductQueryParams queryParams)
        {
            return p => ((!queryParams.brandId.HasValue || p.ProductBrandId == queryParams.brandId.Value)
            && (!queryParams.typeId.HasValue || p.ProductTypeId == queryParams.typeId.Value))
            && ((string.IsNullOrEmpty(queryParams.search)) || p.Name.ToLower().Contains(queryParams.search.ToLower()));


        }
    }
}
