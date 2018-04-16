using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lista2.Cache
{
    public class RandCache : CacheAbstract
    {
        private List<int> _cache;
        private Random _random;
        public RandCache(int k) : this(k, new Random()) { }

        public RandCache(int k, Random random) : base(k)
        {
            _cache = new List<int>();
            _random = random;
        }

        public override int GetValue(int n)
        {
            if (_cache.Any(x => x == n)) return 0;

            if(_cache.Count() == _k)
                _cache.Remove(_cache[_random.Next(_k)]);

            _cache.Add(n);
            return 1;
        }
    }
}
