using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Services.Abstraction
{
    public interface ICacheService
    {
        Task<string?> GetAsync(string cachekey);
        Task SetAsync(string cachekey, object cacheValue,TimeSpan timeToLive);
    }
}
