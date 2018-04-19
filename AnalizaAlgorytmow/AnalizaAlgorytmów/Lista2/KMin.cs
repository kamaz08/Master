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
        //private MultisetAbstract<int> _multisetAbstract;
        //private IHashFunction _hashFunction;

        //public KMin(IHashFunction hashFunction, MultisetAbstract<int> multiset)
        //{
        //    _hashFunction = hashFunction;
        //    _multisetAbstract = multiset;
        //}

        //public List<ItemDouble> Test()
        //{
        //    var result = new List<ItemDouble>();
        //    for (int i = 100; i < _multisetAbstract.GetLength(); i += 100)
        //    {
        //        var _M = new List<BigInteger>();
        //        var set = _multisetAbstract.GetMultiset(i);
        //        set.ForEach(item =>
        //        {
        //            var hash = _hashFunction.GetHash(item);
        //            if (_M.Where(x => x == hash).Count() == 0)
        //            {
        //                _M.Add(hash);
        //                _M = _M.OrderBy(x => x).Take(k).ToList();
        //            }
        //        });
        //        result.Add(new ItemDouble
        //        {
        //            Key = i + 1,
        //            Value = _M.Count() < k ? _M.Count() : ((k - 1) * HashFunction.Propability(_M[k - 1]))
        //        });
        //    }
        //    return result;
        //}
    }
}
