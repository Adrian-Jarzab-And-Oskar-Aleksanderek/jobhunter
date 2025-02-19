using Microsoft.Extensions.Caching.Distributed;

namespace Backend.Service.Caching;

public class RedisService
{
    private readonly IDistributedCache _cache;

    public RedisService(IDistributedCache cache)
    {
        _cache = cache;
    }

    public async Task<string?> GetData(string key)
    {
        return await _cache.GetStringAsync(key);

    }

    public async Task SaveData(string key, string value, TimeSpan absoluteExpirationRelativeToNow, TimeSpan slidingExpiration)
    {
        var cacheOptions = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = absoluteExpirationRelativeToNow,
            SlidingExpiration = slidingExpiration
        };
        await _cache.SetStringAsync(key, value, cacheOptions);
    }
}