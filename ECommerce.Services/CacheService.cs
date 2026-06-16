using ECommerce.Domain.Contracts;
using ECommerce.Services.Abstraction;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace ECommerce.Services
{
    public class CacheService : ICacheService
    {
        private readonly ICacheRepository _cacheRepository ;

        public CacheService(ICacheRepository cacheRepository)
        {
            _cacheRepository = cacheRepository;
        }

        public async Task<string?> GetAsync(string cachekey)
        {
           return await _cacheRepository.GetAsync(cachekey);
        }

        public async Task SetAsync(string cachekey, object cacheValue, TimeSpan timeToLive)
        {
            var value = JsonSerializer.Serialize(cacheValue,new JsonSerializerOptions() { 
            PropertyNamingPolicy=JsonNamingPolicy.CamelCase
            });
            await _cacheRepository.SetAsync(cachekey, value, timeToLive);
        }
    }
}
