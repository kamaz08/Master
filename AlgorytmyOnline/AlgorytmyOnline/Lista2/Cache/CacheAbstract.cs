using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lista2.Cache
{
    public abstract class CacheAbstract : ICache
    {
        protected int _k;

        public CacheAbstract(int k)
        {
            _k = k;
        }

        public abstract int GetValue(int n);
    }
}
