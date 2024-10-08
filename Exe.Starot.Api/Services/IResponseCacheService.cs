using System.Runtime.CompilerServices;

namespace Exe.Starot.Api.Services
{
    public interface IResponseCacheService 
    {
        Task SetCacheResponseAsync(string cachekey, object response, TimeSpan timeOut);
        Task<string> GetCachedResponseAsync(string cacheKey);

        Task RemoveCacheResponseAsync(string pattern);
    }
}
