using AutoMapper;
using ECommerce.Domain.Entities.ProductModule;
using ECommerce.Shared.DTOs.ProductDTOS;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Services.MappingProfiles
{
    public class ProductPictureUrlResolver : IValueResolver<Product, ProductDTO, string>
    {
        private readonly IConfiguration _configuration;

        public ProductPictureUrlResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string Resolve(Product source, ProductDTO destination, string destMember, ResolutionContext context)
        {
            if (string.IsNullOrEmpty(source.PictureUrl)) 
                return string.Empty;

            if (source.PictureUrl.StartsWith("http")||source.PictureUrl.StartsWith("https"))
                return source.PictureUrl;
            var baseUrl = _configuration.GetSection("URLs")["BaseUrl"];
            var pictureUrl=$"{baseUrl}{ source.PictureUrl}";
            return pictureUrl;
        }
    }
}
