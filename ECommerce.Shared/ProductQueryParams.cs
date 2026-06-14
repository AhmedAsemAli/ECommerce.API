using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Shared
{
    public class ProductQueryParams
    {
        public int? brandId { get; set; }
        public int? typeId { get; set; }
        public string? search { get; set; }
        public ProductSortingOptions sort { get; set; }

        private int _pageIndex = 1;

        public int PageIndex
        {
            get { return _pageIndex; }
            set { _pageIndex = (value <= 0) ? 1 : value; }
        }



        private const int MaxPageSize = 10;
        private int _pageSize = 5;

        public int PageSize
        {
            get => _pageSize;
            set
            {
                if (value <= 0)
                    _pageSize = 5;
                else if (value >= 10)
                    _pageSize = MaxPageSize;
                else
                    _pageSize = value;
            }
        }
    }
}
