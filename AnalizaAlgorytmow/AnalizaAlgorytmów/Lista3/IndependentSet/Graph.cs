using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lista3.IndependentSet
{
    public class Verticle
    {
        public bool MSI { get; set; }
        public List<Verticle> Neighbours { get; set; }
        public int Id { get; set; }
        public Verticle(int id)
        {
            Id = id;
            MSI = false;
            Neighbours = new List<Verticle>();
        }
    }

    public class Graph
    {
        private List<Verticle> _verticleList;
        private Random _random;

        public Graph(int n, int deg) : this(n, deg, new Random())
        {
        }

        public Graph(int n, int deg, Random random)
        {
            _random = random;

            _verticleList = new List<Verticle>();
            for (int i = 0; i < n; i++)
                _verticleList.Add(new Verticle(i));

            for (int i = 0; i < n * deg; i++)
            {
                var v1 = _random.Next(n);
                var v2 = _random.Next(n - 1);

                var temp = _verticleList.Where(x => x != _verticleList[v1]).ToList();

                if (_verticleList[v1].Neighbours.Any(x => x == temp[v2])) continue;

                _verticleList[v1].Neighbours.Add(temp[v2]);
                temp[v2].Neighbours.Add(_verticleList[v1]);
            }
        }

        public void WriteGraph()
        {
            _verticleList.ForEach(x =>
            {
                Console.Out.Write($"{x.Id}: ");
                x.Neighbours.ForEach(y => Console.Out.Write($" {y.Id}"));
                Console.Out.WriteLine();
            });
        }


        public void IndependentSet()
        {
            int count = 0, n = _verticleList.Count();
            while (count <= n)
            {
                _verticleList.ForEach(v1 =>
                {
                    if ((v1.MSI && v1.Neighbours.Any(x => x.MSI)) || (v1.MSI == false && v1.Neighbours.All(x=> x.MSI==false)))
                    {
                        v1.MSI = !v1.MSI;
                        count = 0;
                    }
                    else count++;
                });
            }

            Console.Out.WriteLine("\nIndependent set:");
            _verticleList.Where(x => x.MSI).ToList().ForEach(x => Console.Out.Write($" {x.Id}"));
            Console.Out.WriteLine();
        }

        //public void Inde


    }
}
