using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chart.L2
{
    public class Result
    {
        public int Size { get; set; }
        public double First { get; set; }
        public double Second { get; set; }
    }

    public class Vertex
    {
        public int Id { get; set; }
        public List<Vertex> Neighbors { get; set; }

        public Vertex(int id)
        {
            Id = id;
            Neighbors = new List<Vertex>();
        }
    }

    public class Graph
    {
        private int _numberOfVerticles;
        private double _propability;
        private Random _random;
        private Dictionary<int, Vertex> Vertexs;

        public Graph(int size, double propability) : this(size, propability, new Random()) { }

        public Graph(int size, double propability, Random random)
        {
            _numberOfVerticles = size;
            _propability = propability;
            _random = random;
        }

        public void Generate()
        {
            Vertexs = Enumerable.Range(1, _numberOfVerticles).Select(x => new Vertex(x)).ToDictionary(x => x.Id);
            Enumerable.Range(1, _numberOfVerticles - 1).ToList().ForEach(first =>
            {
                Enumerable.Range(first + 1, _numberOfVerticles - first).ToList().ForEach(second =>
                {
                    if (GetProbality())
                    {
                        AddEdge(first, second);
                    }
                });
            });
        }

        public Result GetResult()
        {
            var visited = new HashSet<int>();
            var res = Enumerable.Range(1, _numberOfVerticles - 1).Select(id1 =>
            {
                if (visited.Contains(id1)) return 0;

                var actual = new HashSet<int> { id1 };
                var next = new HashSet<int>();

                Vertexs[id1].Neighbors.ForEach(id2 =>
                {
                    if (!next.Contains(id2.Id)) next.Add(id2.Id);
                });

                while(next.Count > 0)
                {
                    var dep = next.First();
                    next.Remove(dep);
                    actual.Add(dep);

                    Vertexs[dep].Neighbors.ForEach(x =>
                    {
                        if (!(actual.Contains(x.Id) || next.Contains(x.Id)))
                            next.Add(x.Id);
                    });
                }
                foreach (var x in actual)
                    visited.Add(x);
                return actual.Count;
            }).OrderByDescending(x=>x).Take(2).ToList();

            return new Result
            {
                Size = _numberOfVerticles,
                First = res[0],
                Second = res[1]
            };
        }



        private bool GetProbality()
        {
            return _propability > _random.NextDouble();
        }

        private void AddEdge(int id, int id2)
        {
            Vertexs[id].Neighbors.Add(Vertexs[id2]);
            Vertexs[id2].Neighbors.Add(Vertexs[id]);
        }

    }
}
