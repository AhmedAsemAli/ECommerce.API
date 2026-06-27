using AutoMapper;
using ECommerce.Domain.Contracts;
using ECommerce.Domain.Entities.OrderModule;
using ECommerce.Domain.Entities.ProductModule;
using ECommerce.Services.Abstraction;
using ECommerce.Services.Specifications.OrderSpecifications;
using ECommerce.Shared.CommonResponses;
using ECommerce.Shared.DTOs.BasketDTOs;
using Microsoft.Extensions.Configuration;
using Stripe;
using Stripe.Forwarding;
using System;
using System.Collections.Generic;
using System.Text;
using Product = ECommerce.Domain.Entities.ProductModule.Product;

namespace ECommerce.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly  IConfiguration _configuration ;
        private readonly   IMapper _mapper ;

        public PaymentService(IBasketRepository basketRepository, IUnitOfWork unitOfWork, IConfiguration configuration, IMapper mapper)
        {
            _basketRepository = basketRepository;
            _unitOfWork = unitOfWork;
            _configuration = configuration;
            _mapper = mapper;
        }

        public async Task<Result<BasketDTO>> CreateOrUpdatePaybentIntentAsync(string basketId)
        {
            var sKey = _configuration["Stripe:SKey"];
            
            if (sKey is null)
                return Error.Faliure("Failed to obtain secret key value");

            StripeConfiguration.ApiKey = sKey;

            var basket = await _basketRepository.GetBasketAsync(basketId);

            if (basket is null)
                return Error.NotFound("Basket Not Found");

            if (basket.DeliveryMethodId is null)
                return Error.Validation("Delivery method is not selected in the basket");

            var deliveryMethod = await _unitOfWork.GetRepository<DeliveryMethod, int>().GetByIdAsync(basket.DeliveryMethodId.Value);

            if (deliveryMethod is null)
                return Error.NotFound("Delivery Method Not Found");

            basket.ShippingPrice = deliveryMethod.Price;

            foreach (var item in basket.Items)
            {
                var product = await _unitOfWork.GetRepository<Product, int>().GetByIdAsync(item.Id);

                if (product is null)
                    return Error.NotFound("productItem.Not Found");

                item.Price = product.Price;
                item.ProductName = product.Name;
                item.PictureUrl = product.PictureUrl;

            }

            long amount = (long)(basket.Items.Sum(i => i.Quantity * i.Price) * 100);

            var stripeService = new PaymentIntentService();


            if (basket.PaymentIntentID is null)
            {
                var options = new PaymentIntentCreateOptions()
                {
                    Amount = amount,
                    Currency = "USD",
                    PaymentMethodTypes = ["card"]
                };
                var paymentIntent= await stripeService.CreateAsync(options);

                basket.PaymentIntentID = paymentIntent.Id;
                basket.ClientSecret = paymentIntent.ClientSecret;

            }
            else
            {
                var options = new PaymentIntentUpdateOptions()
                {
                    Amount=amount
                };
                await stripeService.UpdateAsync(basket.PaymentIntentID,options);
            }
            await _basketRepository.CreateOrUpdateBasketAsync(basket);


            return _mapper.Map<BasketDTO>(basket);
        }

        public async Task UpdateOrderPaymentStatus(string request, string stripeSignature)
        {
            var endpointSecret = _configuration["Stripe:EndpointSecret"];
            var stripeEvent = EventUtility.ConstructEvent(request, stripeSignature, endpointSecret);

            var paymentIntent = stripeEvent.Data.Object as PaymentIntent;
            var order = await _unitOfWork.GetRepository<Order, Guid>().GetByIdAsync(new OrderWithPaymentIntentSpecification(paymentIntent!.Id));

            // Handle the event
            if (stripeEvent.Type == EventTypes.PaymentIntentSucceeded)
            {
                order.Status = OrderStatus.PaymentReceived;
                _unitOfWork.GetRepository<Order, Guid>().Update(order);
                await _unitOfWork.SaveChangesAsync();
             }
            else if(stripeEvent.Type == EventTypes.PaymentIntentPaymentFailed)
            {
                order.Status = OrderStatus.PaymentFailed;
                _unitOfWork.GetRepository<Order, Guid>().Update(order);
                await _unitOfWork.SaveChangesAsync();

            }
            // ... handle other event types
            else
            {
                Console.WriteLine("Unhandled event type: {0}", stripeEvent.Type);
            }
        }
    }
}
