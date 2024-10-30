using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using StackExchange.Redis;
using System.Data.Common;
using WebApi.Models;
using WebApi.Services.Abstract;
using WebApi.Services.Concrete;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CacheController : ControllerBase
    {
        private readonly IRedisCacheService _redisCacheService;

        public CacheController(IRedisCacheService redisCacheService)
        {
            _redisCacheService = redisCacheService;
        }

        //GET api/redis/{key}
        [HttpGet("{key}")]
        public ActionResult<string> GetValue(string key)
        {
            var value = _redisCacheService.GetValue(key);
            if (value is null)
            {
                return NotFound();
            }
            return Ok(value);
        }

        [HttpPost]
        public ActionResult SetValues([FromBody] List<KeyValue> keyValuePairs)
        {
            foreach (var kvp in keyValuePairs)
            {
                _redisCacheService.SetValue(kvp.Key, kvp.Value);
            }
            return Created("Values added to Redis", null);
        }
    }
}
