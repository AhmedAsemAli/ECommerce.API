using ECommerce.Domain.Contracts;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Presistence.Repositories
{
    public class CacheRepository : ICacheRepository
    {
        private readonly IDatabase _database ;

        public CacheRepository(IConnectionMultiplexer connection)
        {
            _database = connection.GetDatabase();
        }

        public async Task<string?> GetAsync(string cacheKey)
        {
           var cacheValue=await _database.StringGetAsync(cacheKey);
            if (cacheValue.IsNullOrEmpty)
            {
                return null;
            }
            return cacheValue.ToString();
        }

        public async Task SetAsync(string cacheKey, string cacheValue, TimeSpan timeToLive)
        {
           await _database.StringSetAsync(cacheKey, cacheValue, timeToLive);
        }
    }
}
