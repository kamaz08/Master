using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lista2.Cache
{
    public class RMACache : CacheAbstract
    {
        class ItemBool {
            public int N { get; set; }
            public bool Selected { get; set; }
        }

        private List<ItemBool> _cache;
        private Random _random;


        public RMACache(int k) : this(k, new Random()) { }
        public RMACache(int k, Random random) : base(k)
        {
            _random = random;
            _cache = new List<ItemBool>();
        }

        public override int GetValue(int n)
        {
            if (_cache.Any(x => x.N == n))
            {
                _cache.First(x => x.N == n).Selected = true;
                return 0;
            }
            if(_cache.Count() == _k)
            {
                var unSelected = _cache.Where(x => x.Selected == false).ToList();
                if(unSelected.Count() != 0)
                    _cache.Remove(unSelected[_random.Next(unSelected.Count())]);
                else
                {
                    _cache.ForEach(x => x.Selected = false);
                    _cache.Remove(_cache[_random.Next(_cache.Count())]);
                }
            }

            _cache.Add(new ItemBool { N = n, Selected = true });
            return 1;
        }
    }
}
