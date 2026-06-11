using ECommerce.Shared;
using ECommerce.Shared.DTOs.ProductDTOS;
using System;
using System.Collections.Generic;
using System.Text;


namespace ECommerce.Services.Abstraction
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDTO>> GetAllProductsAsync(ProductQueryParams queryParams);
        Task<ProductDTO> GetProductByIdAsync(int id);

        Task<IEnumerable<BrandDTO>> GetAllBrandsAsync();
        Task<IEnumerable<TypeDTO>> GetAllTypesAsync();
    }
}
