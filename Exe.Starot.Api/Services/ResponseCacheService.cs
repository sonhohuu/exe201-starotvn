﻿
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using StackExchange.Redis;

namespace Exe.Starot.Api.Services
{
    public class ResponseCacheService : IResponseCacheService
    {
        private readonly IDistributedCache _distributedCache;
        private readonly IConnectionMultiplexer _connectionMultiplexer;
        public ResponseCacheService(IDistributedCache distributedCache, IConnectionMultiplexer connectionMultiplexer)
        {
            _distributedCache = distributedCache;
            _connectionMultiplexer = connectionMultiplexer;
        }
    
        public async Task<string> GetCachedResponseAsync(string cacheKey)
        {
            var cacheResponse = await _distributedCache.GetStringAsync(cacheKey);
            return string.IsNullOrEmpty(cacheResponse) ? null : cacheResponse;
        }

        public async Task SetCacheResponseAsync(string cachekey, object response, TimeSpan timeOut)
        {
            if (response == null)
            {
                return;

                var serializerResponse = JsonConvert.SerializeObject(response, new JsonSerializerSettings()
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()

                });
                await _distributedCache.SetStringAsync(cachekey, serializerResponse, new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = timeOut
                });
            }
        }
    }
}
