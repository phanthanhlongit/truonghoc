using System;
using Microsoft.Extensions.Caching.Memory; // Import the required namespace

namespace KidsSchool.Models.Helpers
{
    // Make MemoryCacheManager a static class
    public static class MemoryCacheManager
    {
        // Create a static MemoryCache instance
        private static readonly MemoryCache _cache = new MemoryCache(new MemoryCacheOptions());

        // Create a static method to get data from the cache
        public static T Get<T>(string key)
        {
            if (_cache.TryGetValue(key, out T value))
            {
                return value;
            }
            return default(T);
        }

        // Create a static method to set data in the cache
        public static void Set<T>(string key, T value, DateTimeOffset absoluteExpiration)
        {
            _cache.Set(key, value, absoluteExpiration);
        }

        // Create a static method to remove data from the cache
        public static void Remove(string key)
        {
            _cache.Remove(key);
        }
    }
}