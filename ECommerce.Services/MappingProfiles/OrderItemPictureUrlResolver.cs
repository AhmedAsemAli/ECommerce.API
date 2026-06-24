using AutoMapper;
using ECommerce.Domain.Entities.OrderModule;
using ECommerce.Shared.DTOs.OrderDTOs;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Services.MappingProfiles
{
    internal class OrderItemPictureUrlResolver : IValueResolver<OrderItem, OrderItemDTO, string>
    {
        private readonly IConfiguration _configuration;

        public OrderItemPictureUrlResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string Resolve(OrderItem source, OrderItemDTO destination, string destMember, ResolutionContext context)
        {
            if (string.IsNullOrEmpty(source.Product.PictureUrl))
                return string.Empty;

            if (source.Product.PictureUrl.StartsWith("http") || source.Product.PictureUrl.StartsWith("https"))
                return source.Product.PictureUrl;
            var baseUrl = _configuration.GetSection("URLs")["BaseUrl"];
            if (string.IsNullOrEmpty(baseUrl))
                return string.Empty;
            var pictureUrl = $"{baseUrl}{source.Product.PictureUrl}";
            return pictureUrl;
        }
    }
}
