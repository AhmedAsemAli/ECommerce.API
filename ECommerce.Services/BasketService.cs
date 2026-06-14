using AutoMapper;
using ECommerce.Domain.Contracts;
using ECommerce.Domain.Entities.BasketModule;
using ECommerce.Services.Abstraction;
using ECommerce.Shared.DTOs.BasketDTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Services
{
    public class BasketService : IBasketService
    {
        private readonly IBasketRepository _basketRepository ;
        private readonly IMapper _mapper ;

        public BasketService(IBasketRepository basketRepository, IMapper mapper)
        {
            _basketRepository = basketRepository;
            _mapper = mapper;
        }

        public async Task<BasketDTO> CreateOrUpdateBasketAsync(BasketDTO createOrUpdatedBasket)
        {
            var customerBasket = _mapper.Map<CustomerBasket>(createOrUpdatedBasket);
            var CreateOrUpdateBasket= await _basketRepository.CreateOrUpdateBasketAsync(customerBasket);

            return _mapper.Map<BasketDTO>(CreateOrUpdateBasket);
        }

        public async Task<bool> DeleteBasketAsync(string basketId)
        {
            return await _basketRepository.DeleteBasketAsync(basketId);
        }

        public async Task<BasketDTO> GetBasketAsync(string basketId)
        {
            var basket=await _basketRepository.GetBasketAsync(basketId);
            return _mapper.Map<BasketDTO>(basket);
        }
    }
}
