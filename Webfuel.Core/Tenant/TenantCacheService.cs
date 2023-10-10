using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webfuel
{
    public interface ITenantCacheService
    {
        T? Get<T>(string key) where T : class;

        T Set<T>(string key, T item) where T : class;

        void Remove(string key);

        void Clear();
    }

    [Service(typeof(ITenantCacheService))]
    internal class TenantCacheService: ITenantCacheService
    {
        private readonly ITenantAccessor _tenantAccessor;

        public TenantCacheService(ITenantAccessor tenantAccessor)
        {
            _tenantAccessor = tenantAccessor;
        }

        public T? Get<T>(string key) where T : class
        {
            return Cache.Get(BuildKey(key)) as T;
        }

        public T Set<T>(string key, T item) where T : class
        {
            Cache.Set(BuildKey(key), item);
            return item;
        }

        public void Remove(string key)
        {
            Cache.Remove(BuildKey(key));
        }

        public void Clear()
        {
            Cache.Clear();
        }

        // Implementation

        string BuildKey(string key)
        {
            return _tenantAccessor.Tenant.Id + ":" + key;
        }

        static MemoryCache Cache = new MemoryCache(new MemoryCacheOptions
        {
            // Default Options
        });
    }
}
