using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lista2.Cache
{
    public class FIFOCache : CacheAbstract
    {
        private List<int> _cache;


        public FIFOCache(int k) : base(k)
        {
            _cache = new List<int>();
        }

        public override int GetValue(int n)
        {
            if (_cache.Any(x => x == n)) return 0;

            _cache.Add(n);
            if (_cache.Count() > _k)
                _cache = _cache.Skip(1).ToList();
            return 1;
        }
    }
}
