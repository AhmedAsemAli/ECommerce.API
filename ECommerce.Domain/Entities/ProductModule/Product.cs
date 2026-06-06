using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Domain.Entities.ProductModule
{
    public class Product : BaseEntity<int>
    {
        public string Name { get; set; } = default!;
        public string Description { get; set; }=default!;
        public string PictureUrl { get; set; } = default!;
        public decimal  Price { get; set; }

        public int ProductBrandId { get; set; }
        public ProductBrand ProductBrand { get; set; } = null!;
        public int ProductTypeId { get; set; }
        public ProductType ProductType { get; set; }=null!;
    }
}
