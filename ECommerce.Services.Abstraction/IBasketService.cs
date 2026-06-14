using ECommerce.Shared.DTOs.BasketDTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Services.Abstraction
{
    public interface IBasketService
    {
        Task<BasketDTO> CreateOrUpdateBasketAsync(BasketDTO createOrUpdatedBasket);
        Task<BasketDTO> GetBasketAsync(string basketId);
        Task<bool> DeleteBasketAsync(string basketId);
    }
}
