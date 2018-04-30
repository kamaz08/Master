using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using Lista2.Model;

namespace Lista2.Hash
{
    public static class HyperLogLog
    {
        private static double GetAlfa(int b)
        {
            switch (b)
            {
                case 4: return 0.673;
                case 5: return 0.697;
                case 6: return 0.709;
                default: return 0.7;
            }
        }
        public static Dictionary<int, Dictionary<decimal, int>> Execute(int b, IHashFunctionByte func, MultisetAbstract set)
        {
            var result = new Dictionary<int, Dictionary<decimal, int>>();
            for (int i = 10000; i > 0; i--)
            {
                var ires = new Dictionary<decimal, int>();
                var s = set.GetMultiset(i - 1);
                s.ForEach(x => AddElement(b, ires, func.GetHash(x)));
                result.Add(i, ires);
            }
            return result;
        }

        private static void AddElement(int b, Dictionary<decimal, int> dict, BitArray value)
        {
            decimal v = 0;
            for (var i = 0; i < b; i++)
                if (value[i]) v += (decimal)Math.Pow(2, i);

            var m = b;
            while (value.Length > m && !value[m]) m++;
            m -= b - 1;

            if (dict.ContainsKey(v))
                dict[v] = dict[v] < m ? m : dict[v];
            else
                dict.Add(v, m);
        }

        public static double GetForK(int b, Dictionary<decimal, int> dict)
        {
            var m = Math.Pow(2.0, b);
            var list = GetListForK(b, dict).OrderBy(x => x).ToList();

            double nn = GetAlfa(b) * m * m * Math.Pow(list.Sum(x => Math.Pow(2.0, -(x))) + m - list.Count(),-1);


            if (nn < 2.5 * m)
            {
                //var m = Math.Pow(2.0, b);
                double v = m - list.Count();
                if(v > 0)
                    nn = m * Math.Log(m / v);
            }

            double pp = Math.Pow(2, 32);
            if (nn > 0.3333334 * pp)
            {
                double x = nn / pp;
                nn = pp * Math.Log(1.0 - x);
            }
            return nn;
        }

        private static List<int> GetListForK(int b, Dictionary<decimal, int> dict)
        {
            var k = (int)Math.Pow(2, b);
            var list = dict
                .ToList()
                .Select(x => new KeyValuePair<decimal, int>(x.Key % k, x.Value));
            return list.GroupBy(x => x.Key)
                .Select(x => x.Max(y => y.Value))
                .ToList();
        }

        public static List<ItemDouble> GetResult(Dictionary<int, Dictionary<decimal, int>> dict, int b)
        {
            var result = new List<ItemDouble>();

            for (int i = 1; i <= 10000; i++)
                result.Add(new ItemDouble
                {
                    Key = i,
                    Value = GetForK(b, dict[i])
                });
            return result;
        }

    }
}
