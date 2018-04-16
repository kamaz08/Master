using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lista2.Cache
{
    public class LFUCache : CacheAbstract
    {
        class ItemCount
        {
            public int N { get; set; }
            public int Count { get; set; }
        }

        private List<ItemCount> _cache;

        public LFUCache(int k) : base(k)
        {
            _cache = new List<ItemCount>();
        }

        public override int GetValue(int n)
        {
            var tempCache = _cache.OrderByDescending(x => x.Count).ToList();
            if (tempCache.Take(_k).Any(x=>x.N == n))
            {
                tempCache.First(x => x.N == n).Count++;
                return 0;
            }
            var temp = _cache.FirstOrDefault(x => x.N == n);
            if (temp != null) temp.Count++;
            else _cache.Add(new ItemCount { N = n, Count = 1 });
            return 1;
        }
    }
}
