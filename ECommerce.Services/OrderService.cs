using AutoMapper;
using ECommerce.Domain.Contracts;
using ECommerce.Domain.Entities.BasketModule;
using ECommerce.Domain.Entities.OrderModule;
using ECommerce.Domain.Entities.ProductModule;
using ECommerce.Services.Abstraction;
using ECommerce.Services.Specifications.OrderSpecifications;
using ECommerce.Shared.CommonResponses;
using ECommerce.Shared.DTOs.OrderDTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Services
{
    public class OrderService : IOrderService
    {
        private readonly IMapper _mapper;
        private readonly IBasketRepository _basketRepository ;
        private readonly IUnitOfWork _unitOfWork ;

        public OrderService(IMapper mapper, IBasketRepository basketRepository, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _basketRepository = basketRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<OrderToReturnDTO>> CreateOrderAsync(OrderDTO orderDTO, string email)
        {
            var orderAddress=_mapper.Map<OrderAddress>(orderDTO.Address);

            var basket=await _basketRepository.GetBasketAsync(orderDTO.BasketId);
            if (basket is null)
                return Error.NotFound("Basket Not Found",$"the basket with id {orderDTO.BasketId} is not found");

            List<OrderItem> orderItems= new List<OrderItem>();
            foreach (var item in basket.Items)
            {

                var product = await _unitOfWork.GetRepository<Product, int>().GetByIdAsync(item.Id);

                if (product is null)
                    return Error.NotFound("product Not Found", $"the product with id {item.Id} is not found");
              orderItems.Add(CreateOrderItem(item, product));

            }

            var delivaryMethod = await _unitOfWork.GetRepository<DeliveryMethod, int>().GetByIdAsync(orderDTO.DeliveryMethodId);
            if (delivaryMethod is null)
                return Error.NotFound("delivaryMethod Not Found", $"the delivaryMethod with id {orderDTO.DeliveryMethodId} is not found");

            var subTotal = orderItems.Sum(x => x.Price * x.Quantity);

            var order=new Order()
            {
                UserEmail = email,
                Address = orderAddress,
                DeliveryMethod = delivaryMethod,
                SubTotal = subTotal,
                Items = orderItems,
                

            };
            await _unitOfWork.GetRepository<Order, Guid>().AddAsync(order);
            bool result=await _unitOfWork.SaveChangesAsync()>0;
            if (!result)
                Error.Faliure("Order Is Faliure", "there was a problem while creating the order");
        
        
            return _mapper.Map<OrderToReturnDTO>(order);
        
        }

        public async Task<Result<IEnumerable<DeliveryMethodDTO>>> GetAllDeliveryMethodsAsync()
        {
            var deliveryMethods =await _unitOfWork.GetRepository<DeliveryMethod, int>().GetAllAsync();

            if (!deliveryMethods.Any())
                return Error.NotFound("delivary Method Not Found", "no delivary Method was found");

            var data = _mapper.Map<IEnumerable<DeliveryMethod>, IEnumerable<DeliveryMethodDTO>>(deliveryMethods);

            if (data is null)
                return Error.NotFound("delivary Method Not Found", "no delivary Method was found");

            return Result<IEnumerable<DeliveryMethodDTO>>.Ok(data);

        }

        public async Task<Result<IEnumerable<OrderToReturnDTO>>> GetAllOrdersAsync(string email)
        {
            var orderSpec = new OrderSpecification(email);
            var orders = await _unitOfWork.GetRepository<Order, Guid>().GetAllAsync(orderSpec);
            if (!orders.Any())
                return Error.NotFound("Orders Not Found", $"No Orders found for the user with email {email}");

            var data = _mapper.Map<IEnumerable<Order>,IEnumerable<OrderToReturnDTO>>(orders);
            return Result<IEnumerable<OrderToReturnDTO>>.Ok(data);
        }

        public async Task<Result<OrderToReturnDTO>> GetOrderByIdAsync(Guid id, string email)
        {
           
            var orderSpec=new OrderSpecification(id,email);
            var order = await _unitOfWork.GetRepository<Order, Guid>().GetByIdAsync(orderSpec);
            if  (order is null)
                return Error.NotFound("Orders Not Found", $"No Orders found for the user with email {email}");

            var data = _mapper.Map<Order, OrderToReturnDTO>(order);
            return Result<OrderToReturnDTO>.Ok(data);
        }

        private  OrderItem CreateOrderItem(BasketItem item, Product product)
        {
            return  new OrderItem()
            {
                Product = new ProductItemOrdered()
                {
                    ProductId = product.Id,
                    ProductName = product.Name,
                    PictureUrl = product.PictureUrl,
                },
                Price = product.Price,
                Quantity = item.Quantity,

            };
        }
    }
}
