using StackExchange.Redis;
using WebApi.Services.Abstract;

namespace WebApi.Services.Concrete
{
    public class RedisCacheService : IRedisCacheService
    {
        private readonly IDatabase _database;

        public RedisCacheService(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }

        public void SetValue(string key, string value)
        {
            _database.StringSet(key, value);
        }

        public string GetValue(string key)
        {
            return _database.StringGet(key);
        }
    }
}
