using AutoMapper;
using ECommerce.Domain.Contracts;
using ECommerce.Domain.Entities.ProductModule;
using ECommerce.Services.Abstraction;
using ECommerce.Services.Specifications.ProductSpecifications;
using ECommerce.Shared;
using ECommerce.Shared.DTOs.ProductDTOS;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BrandDTO>> GetAllBrandsAsync()
        {
            var brands =await _unitOfWork.GetRepository<ProductBrand, int>().GetAllAsync();
            return _mapper.Map < IEnumerable < BrandDTO >> (brands);
        }

        public async Task<IEnumerable<ProductDTO>> GetAllProductsAsync(ProductQueryParams queryParams)
        {
            var spec = new ProductWithTypeAndBrandSpecification(queryParams);
            var products =await _unitOfWork.GetRepository<Product, int>().GetAllAsync(spec);
            return _mapper.Map<IEnumerable < ProductDTO>> (products);
        }

        public async Task<IEnumerable<TypeDTO>> GetAllTypesAsync()
        {
            var types =await _unitOfWork.GetRepository<ProductType, int>().GetAllAsync();
            return _mapper.Map < IEnumerable<TypeDTO>>(types);
        }

        public async Task<ProductDTO> GetProductByIdAsync(int id)
        {
            var spec = new ProductWithTypeAndBrandSpecification(id);
            var product=await _unitOfWork.GetRepository<Product,int>().GetByIdAsync(spec);
       
            return _mapper.Map<ProductDTO> (product);
        }
    }
}
