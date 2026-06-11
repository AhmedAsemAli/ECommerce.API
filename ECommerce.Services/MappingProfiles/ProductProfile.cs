using AutoMapper;
using ECommerce.Domain.Entities.ProductModule;
using ECommerce.Shared.DTOs.ProductDTOS;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Services.MappingProfiles
{
    internal class ProductProfile:Profile
    {
        public ProductProfile()
        {
           
            CreateMap<ProductBrand,BrandDTO>();
           
            CreateMap<Product,ProductDTO>()
                .ForMember(dest=>dest.ProductBrand,opt=>opt.MapFrom(src=>src.ProductBrand.Name))
                .ForMember(dest=>dest.ProductType,opt=>opt.MapFrom(src=>src.ProductType.Name))
                .ForMember(dest=>dest.PictureUrl,opt=>opt.MapFrom<ProductPictureUrlResolver>());
           
            CreateMap<ProductType, TypeDTO>();

        }
    }
}
