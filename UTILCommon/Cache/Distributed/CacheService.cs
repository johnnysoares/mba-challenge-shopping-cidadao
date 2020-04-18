using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using UTILCommon.Extensions;

namespace UTILCommon.Cache.Distributed {

    public class CacheService : ICacheService {

        //
        private readonly IDistributedCache cache;

        /// <summary>
        /// Construtor
        /// </summary>
        public CacheService([FromServices]IDistributedCache _cache) {

            cache = _cache;
        }

        /// <summary>
        /// 
        /// </summary>
        private string createKey(string key) {

            return $":{key}";
        }

        /// <summary>
        /// 
        /// </summary>
        public async Task set(string key, object dataCache, int minutesCache = 5) {

            string fullKey = this.createKey(key);

            string json = JsonConvert.SerializeObject(dataCache, Formatting.None, new JsonSerializerSettings {
                                                                                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                                                                                        NullValueHandling = NullValueHandling.Ignore
                                                    });

            var options = new DistributedCacheEntryOptions();

            options.SetAbsoluteExpiration(TimeSpan.FromMinutes(minutesCache));

            await this.cache.SetStringAsync(fullKey, json, options);
        }

        /// <summary>
        /// 
        /// </summary>
        public async Task<T> get<T>(string key) {

            string fullKey = this.createKey(key);

            string jsonCache = await this.cache.GetStringAsync(fullKey);

            if (jsonCache.isEmpty()) {

                return default;
            }

            var result = JsonConvert.DeserializeObject<T>(jsonCache);

            return result;
        }
    }
}
