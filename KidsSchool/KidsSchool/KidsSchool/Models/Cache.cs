using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Web;

namespace KidsSchool.Models
{
    public class MemoryCacheManager
    {
        private static readonly MemoryCache Cache = MemoryCache.Default;

        public static T Get<T>(string key)
        {
            return (T)Cache.Get(key);
        }

        public static void Set(string key, object value, DateTimeOffset absoluteExpiration)
        {
            Cache.Set(key, value, absoluteExpiration);
        }

        public static void Remove(string key)
        {
            Cache.Remove(key);
        }
    }

}