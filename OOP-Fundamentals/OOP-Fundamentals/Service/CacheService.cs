using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Caching;
using OOP_Fundamentals.Interface;

namespace OOP_Fundamentals.Service
{
    class CacheService : ICacheService
    {
        private MemoryCache _cache;

        public CacheService() {
            _cache = new MemoryCache("DocumentCache");
        }

        public void AddToCache(string key, IDocument document, TimeSpan duration)
        {
            _cache.Add(key, document, DateTimeOffset.Now.Add(duration));
        }

        public IDocument GetFromCache(string key)
        {
            return _cache.Get(key) as IDocument;
        }
    }
}
