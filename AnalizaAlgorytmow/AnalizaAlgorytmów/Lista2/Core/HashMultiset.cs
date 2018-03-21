using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Lista2.Core
{
    public class HashMultiset
    {
        private List<List<BigInteger>> _sortedSet;
        private IHashFunction _hashFunction;
        public HashMultiset(MultisetAbstract<int> multiset, IHashFunction hashFunction) : this(multiset, hashFunction, 400) { }

        public HashMultiset(MultisetAbstract<int> multiset, IHashFunction hashFunction, int k)
        {
            _hashFunction = hashFunction;
            _sortedSet = new List<List<BigInteger>>();
            for (int i = 0; i < multiset.GetLength(); i+=1)
            {
                var m = multiset.GetMultiset(i);
                var temp = new List<BigInteger>();
                m.ForEach(item =>
                {
                    var hash = hashFunction.GetHash(item);
                    if ((temp.Count() < k && !temp.Any(x => x == hash) || (temp.Count() >= k && temp[k - 1] > hash)))
                    {
                        temp.Add(hash);
                        if (temp.Count() - k > 100) temp = temp.OrderBy(x => x).Take(k).ToList();
                    }
                });
                _sortedSet.Add(temp.OrderBy(x => x).Take(k).ToList());
            }
        }

        public double GetPropability(int n, int k)
        {
            if (_sortedSet[n].Count() < k + 1)
                return _sortedSet[n].Count();
            return (double) (k) * _hashFunction.GetPropa(_sortedSet[n][k]);
        }


    }
}
