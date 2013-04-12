using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Caching
{
    public class Cache : ICache
    {
        private MemoryCache memoryCache;

        public Cache()
        {
            memoryCache = new MemoryCache("_");
        }

        public override void Dispose()
        {
            memoryCache.Dispose();
        }

        public int GetOrCreate<T>(string name, Func<int> getOrCreate) where T : Dimension, new()
        {
            object result;
            string cacheKey = FormatCacheKey<T>(name);
            if (memoryCache.Contains(cacheKey))
            {
                result = memoryCache.Get(cacheKey);
            }
            else
            {
                result = getOrCreate();
            }

            return (int)result;
        }

        internal static string FormatCacheKey<T>(string name) where T : Dimension, new()
        {
            return string.Format("{0}={1}", typeof(T).Name, name.ToLower());
        }
    }
}
