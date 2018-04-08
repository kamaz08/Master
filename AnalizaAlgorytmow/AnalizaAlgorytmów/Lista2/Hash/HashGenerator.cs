using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms.DataVisualization.Charting;

namespace Lista2.Hash
{
    public class HashGenerator
    {
        public Dictionary<int, List<double>> Kmin(int k, HashFunctionAbstract func, MultisetAbstract set)
        {
            var result = new Dictionary<int, List<double>>();
            for (int i = 10000; i > 0; i--)
            {
                var s = set.GetMultiset(i - 1);
                var ires = new List<double>();
                s.ForEach(x =>
                {
                    var prop = func.GetHash(x);
                    ires = AddElement(ires, k, prop);
                });
                result.Add(i, ires);
            }
            return result;
            //var a = k400.Count(x => Math.Abs(1 - (x.Value / x.Key)) > 0.1357) / (double) 10000.0;
        }

        private List<double> AddElement(List<double> list, int maks, double el)
        {
            int i = list.Count() - 1;
            for (; i >= 0; i--)
            {
                if (list[i] == el) return list;
                if (list[i] < el)
                {
                    if (i + 1 == maks) return list; 
                    var res = list.Take(i + 1).ToList();
                    res.Add(el);
                    res.AddRange(list.Skip(i + 1).Take(maks - i - 2));
                    return res;
                }
            }
            if (i == -1)
            {
                var res = new List<double>() { el };
                res.AddRange(list.Take(maks - 1));
                return res;
            }
            if (list.Count() < maks)
                list.Add(el);
            return list;
        }

        public List<ItemDouble> GetForK(int k, Dictionary<int, List<double>> dict)
        {
            var result = new List<ItemDouble>();
            for (int i = 1; i <= 10000; i++)
            {
                var list = dict[i];
                result.Add(new ItemDouble
                {
                    Key = i,
                    Value = list.Count() < k ? list.Count() : (k-1)/list[k-1]
                });
            }
            return result;
        }
    }
}
