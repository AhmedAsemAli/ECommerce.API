using ECommerce.Shared.DTOs.ProductDTOS;
using System;
using System.Collections.Generic;
using System.Text;


namespace ECommerce.Services.Abstraction
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDTO>> GetAllProductAsync();
        Task<ProductDTO> GetProductByIdAsync(int id);

        Task<IEnumerable<BrandDTO>> GetAllBrandAsync();
        Task<IEnumerable<TypeDTO>> GetAllTypeAsync();
    }
}
