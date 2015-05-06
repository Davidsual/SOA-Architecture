using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace DavideTrotta.Wcf.Handlers.Infrastructure
{
    public interface ICacheProvider
    {
        /// <summary>    
        /// Get the object from the Cache    
        /// </summary>    
        /// <param name="key"></param>    
        /// <returns></returns>    
        TValue Get<TValue>(string key);

        /// <summary>    
        /// Put the object in the cache    
        /// </summary>    
        /// <param name="key"></param>    
        /// <param name="value"></param>    
        void Put<TValue>(string key, TValue value);

        /// <summary>    
        /// Remove an item from the Cache.    
        /// </summary>    
        /// <param name="key">The Key of the Item in the Cache to remove.</param>    
        void Remove(string key);

        /// <summary>    
        /// Clear the Cache    
        /// </summary>    
        void Clear();
    }


    public class CacheProvider : ICacheProvider
    {
        private  MemoryCache _cache;
        private const string CacheName = "MyCache";

        public CacheProvider()
        {
            _cache = new MemoryCache(CacheName);
        }

        public TValue Get<TValue>(string key) 
        {
            if (_cache.Contains(key))
                return (TValue)_cache.Get(key);
            return default(TValue);
        }

        public void Put<TValue>(string key, TValue value) 
        {
            _cache.Set(new CacheItem(key, value), new CacheItemPolicy()
            {
                AbsoluteExpiration = new DateTimeOffset(DateTime.Now.AddDays(1))
            });
        }

        public void Remove(string key)
        {
            _cache.Remove(key);
        }

        public void Clear()
        {
            var oldCache = _cache;
            _cache = new MemoryCache(CacheName);
            oldCache.Dispose();
        }

    }
}
