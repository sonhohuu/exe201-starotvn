
using Exe.Starot.Api.Configuration;
using Exe.Starot.Api.Services;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace Exe.Starot.Api.Installer
{
    public class CacheInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            var redisConfiguration = new RedisConfiguration();
            configuration.GetSection("RedisConfiguration").Bind(redisConfiguration);

            services.AddSingleton(redisConfiguration);
            if(!redisConfiguration.Enabled)
            {
                return;
            }
            services.AddSingleton<IConnectionMultiplexer>(_ => ConnectionMultiplexer.Connect(redisConfiguration.ConnectionString));
            services.AddStackExchangeRedisCache(option => option.Configuration = redisConfiguration.ConnectionString);
            services.AddSingleton<IResponseCacheService,ResponseCacheService>();
        }
    }
}
