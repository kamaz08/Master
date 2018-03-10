using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lista1
{


    public class Election
    {
        private int _n;
        private int _u;
        private List<double> _ProbabilityList;
        private Random _Random;

        public Election()
        {
            _Random = new Random();
        }

        public Election(int n, int u) : this()
        {
            _n = n;
            _u = u;
            _ProbabilityList = new List<double>();
            int m = (int) Math.Ceiling(Math.Log(u, 2.0));
            for (int i = 0; i < m; i++)
                _ProbabilityList.Add(1.0 / Math.Pow(2.0, i % m + 1));
        }

        public Election(int n) : this()
        {
            _u = n;
            _ProbabilityList = new List<double>();
            _ProbabilityList.Add(1.0 / _u);
        }

        private bool Check(double prop)
        {
            int count = 0;
            for (int i = 0; i < (_n == 0 ? _u : _n); i++)
                if (_Random.NextDouble() < prop)
                    if (++count > 1)
                        return false;
            return count == 1;
        }

        public ElectionGraph Test(int p)
        {
            var counter = new List<int>();

            for (int i = 0; i < p; i++)
                for (int j = 0; j < _u; j++)
                    if (Check(_ProbabilityList[j % _ProbabilityList.Count]))
                    {
                        counter.Add(j + 1);
                        break;
                    }
            int fail = p - counter.Count();
            var tempE = counter.Average();
            var tempVar = (1.0 / (p - 1)) * counter.Sum(x => Math.Pow((x - tempE), 2));
            var tempPr = (double)(counter.Where(x => x <= _ProbabilityList.Count).Count()) / p;
            var result = new ElectionGraph
            {
                GraphData = counter
                .GroupBy(x => x)
                .Select(x => new Item { Key = x.Key, Count = x.Count() })
                .OrderBy(x => x.Key)
                .ToList(),
                E = tempE,
                Var = tempVar,
                Pr = tempPr,
                Title = _u == 0 ? $"Dane: {_n}" : $"Dane: {_n}, i {_u}",
            };
            return result;
        }
    }
}
