using ECommerce.Services.Abstraction;
using ECommerce.Shared.DTOs.BasketDTOs;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace ECommerce.Presentation.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class BasketsController:ControllerBase
    {
        private readonly IBasketService _basketService;

        public BasketsController(IBasketService basketService)
        {
            _basketService = basketService;
        }

        [HttpGet]
        public async Task<ActionResult<BasketDTO>>GetBasket(string basketId)
        {
            var basket =await _basketService.GetBasketAsync(basketId);
            return Ok(basket);
        }

        [HttpPost]
        public async Task<ActionResult<BasketDTO>> CreateOrUpdateBasket(BasketDTO basket) 
        {
           var Basket=  await _basketService.CreateOrUpdateBasketAsync(basket);
            return Ok(Basket);
        }

        [HttpDelete("{basketId}")]
        public async Task<ActionResult<bool>>DeleteBasket([FromRoute]string basketId)
        {
            var result=await _basketService.DeleteBasketAsync(basketId);
            return Ok(result);
        }
    }
}
