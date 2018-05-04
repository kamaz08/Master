using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lista3.Dijkstra
{
    public class DijkstraTab
    {
        private byte[] _dijkstra;
        private int _numberOfElements;
        private IDijkstraNextStep _dijkstraNextStep;

        public DijkstraTab(int n) : this(n, new DijkstraNextStep(n)) { }
        public DijkstraTab(int n, IDijkstraNextStep dijkstraNextStep)
        {
            _numberOfElements = (int)Math.Pow(n + 1, n);
            _dijkstra = new byte[_numberOfElements];
            _dijkstraNextStep = dijkstraNextStep;
        }

        public int Caluculate()
        {
            var current = new List<int>();
            var next = new List<int>();


            for (int i = 0; i < _numberOfElements; i++)
            {
                byte stage = 1;
                if (_dijkstra[i] != 0) continue;
                var temp = _dijkstraNextStep.GetNextSteps(i);
                if (temp.Count() < 2) continue;
                current = temp.Where(x => _dijkstra[x] < stage).ToList();
                current.ForEach(x => _dijkstra[x] = stage);
                stage++;
                while (current.Count() != 0)
                {
                    var el = current[0];
                    current.Remove(el);

                    temp = _dijkstraNextStep
                        .GetNextSteps(el)
                        .ToList();

                    if (temp.Count() > 1)
                    {
                        var temp2 = temp
                            .Where(x => _dijkstra[x] < stage)
                            .ToList();
                        temp2.ForEach(x => _dijkstra[x] = stage);
                        next.AddRange(temp2);
                    }

                    if (current.Count() == 0)
                    {
                        stage++;
                        current = next;
                        next = new List<int>();
                    }
                }
            }

             return _dijkstra.Max();
        }
    }
}
