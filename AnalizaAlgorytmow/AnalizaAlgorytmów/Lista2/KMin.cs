using Lista1;
using Lista2.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Lista2
{
    public class KMin
    {
        private HashMultiset _multiset;

        public KMin(HashMultiset hashMultiset)
        {
            _multiset = hashMultiset;
        }

        public List<ItemDouble> Test(int n, int k)
        {
            var result = new List<ItemDouble>();
            for (int i = 0; i < 10000; i+=1)
            {
                result.Add(new ItemDouble
                {
                    Key = i + 1,
                    Value = _multiset.GetPropability(i, k-1) / (i+1)
                });
            }
            return result;
        }
    }
}
