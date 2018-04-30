using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lista3.Dijkstra
{
    public class DijkstraAlg
    {
        public Dictionary<int, DijkstraModel> GetDictionary(int n)
        {
            return GetDictionary(n, new List<int>(), new Dictionary<int, DijkstraModel>());
        }

        private Dictionary<int, DijkstraModel> GetDictionary(int n, List<int> list, Dictionary<int, DijkstraModel> res)
        {
            if (list.Count() == n)
            {
                var a = new DijkstraModel(list);
                res.Add(a.Value, a);
            }
            else
                Enumerable.Range(0, n + 1).ToList().ForEach(x =>
                   {
                       var temp = list.Select(a => a).ToList();
                       temp.Add(x);
                       res = GetDictionary(n, temp, res);
                   });
            return res;
        }
    }
}
