using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lista2.Cache
{
    public class FWFCachecs : CacheAbstract
    {
        private List<int> _cache;
        public FWFCachecs(int k) : base(k)
        {
            _cache = new List<int>();
        }

        public override int GetValue(int n)
        {
            if (_cache.Any(x => x == n)) return 0;

            if (_cache.Count() < _k)
                _cache.Add(n);
            else
                _cache = new List<int> { n };
            return 1;
        }
    }
}
