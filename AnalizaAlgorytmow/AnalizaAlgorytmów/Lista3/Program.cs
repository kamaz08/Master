using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Lista3.Dijkstra;

namespace Lista3
{
    class Program
    {
        static void Main(string[] args)
        {
            var a = new DijkstraAlg().GetDictionary(3);

            var he = a.Select(x => x.Value).Where(x => x.NextSteps.Count() != 1);
            var a1 = he.SelectMany(x => x.NextSteps);
            var a2 = a1.Where(x => (!a[x].Visited)).OrderBy(x=>x).Distinct();

            CheckDijkstra.Check(a);

        }
    }
}
