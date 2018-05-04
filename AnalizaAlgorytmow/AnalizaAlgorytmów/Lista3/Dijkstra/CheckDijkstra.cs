using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lista3.Dijkstra
{
    public static class CheckDijkstra
    {
        public static void Check(Dictionary<int, DijkstraModel> dict)
        {
            foreach (var item in dict)
            {
                if(item.Value.Visited) continue;
                var next = item.Value.NextSteps.Where(x => !dict[x].Visited).ToList();
                var visitedSet = new HashSet<int>();
                while (next.Count() != 0)
                {
                    if (item.Value.Value == next[0])
                    {
                        Console.Out.WriteLine("Nie dziala");
                     //   return;
                    }

                    
                    visitedSet.Add(next[0]);
                    foreach (var ne in dict[next[0]].NextSteps)
                    {
                        if(visitedSet.Contains(ne) || next.Contains(ne) || dict[ne].Visited) continue;
                        next.Add(ne);
                    }
                    next = next.Skip(1).ToList();
                }
                item.Value.Visited = true;
            }
        }
    }
}
