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
            :base(ProductSpecificationsHelper.GetCriteria(queryParams))
        {
            AddInclude(p => p.ProductBrand);
            AddInclude(p => p.ProductType);

            switch (queryParams.sort)
            {   
                case ProductSortingOptions.NameAsc:
                    AddOrderBy(p => p.Name);
                    break;
                case ProductSortingOptions.NameDes:
                    AddOrderByDescending(p => p.Name);
                    break;
                case ProductSortingOptions.PriceAsc:
                    AddOrderBy(p => p.Price);
                    break;
                case ProductSortingOptions.PriceDes:
                    AddOrderByDescending(p => p.Price);
                    break;
                default:
                    AddOrderBy(p => p.Id);
                    break;
            }
            ApplyPagination(queryParams.PageIndex, queryParams.PageSize);
        }
        public ProductWithTypeAndBrandSpecification(int id):base(p=>p.Id==id) 
        {
            AddInclude(p => p.ProductBrand);
            AddInclude(p => p.ProductType);


        }

    }
}
