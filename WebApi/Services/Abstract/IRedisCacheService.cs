using StackExchange.Redis;

namespace WebApi.Services.Abstract
{
    public interface IRedisCacheService
    {
        void SetValue(string key, string value);
        string GetValue(string key);

    }
}
