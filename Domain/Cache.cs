using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Cache
    {
        private ObjectCache objectCache;

        public Cache()
        {
            objectCache = MemoryCache.Default;
        }

        public int GetOrCreate<T>(CarsContext context, string name) where T : Dimension, new()
        {
            object result;
            string cacheKey = FormatCacheKey<T>(name);
            if (objectCache.Contains(cacheKey))
            {
                result = objectCache.Get(cacheKey);
            }
            else
            {
                result = context.Set<T>().GetOrCreate(name);
            }

            return (int)result;
        }

        internal static string FormatCacheKey<T>(string name) where T : Dimension
        {
            return string.Format("{0}={1}", typeof(T).Name, name.ToLower());
        }
    }
}
