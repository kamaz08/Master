using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lista3.Dijkstra
{
    public class DijkstraModel
    {
        public int Value { get; set; }
        public List<int> State { get; set; }
        public HashSet<int> NextSteps { get; set; }
        public bool Visited { get; set; }

        public DijkstraModel(List<int> state)
        {
            State = state;
            Value = GetValue(state);
            Visited = false;
            NextSteps = FindNextStep();
        }

        private int GetValue(List<int> state)
        {
            int n = state.Count();
            int i = 0;
            return state.Select(x => x * (int)Math.Pow(n + 1, i++)).Sum();
        }

        private HashSet<int> FindNextStep()
        {
            var result = new HashSet<int>();

            if (State[0] == State[State.Count - 1])
            {
                var temp = State.Select(x => x).ToList();
                temp[0] = (temp[0] + 1) % (temp.Count() + 1);
                result.Add(GetValue(temp));
            }

            for (int i = 1; i < State.Count(); i++)
            {
                if (State[i] != State[i - 1])
                {
                    var temp = State.Select(x => x).ToList();
                    temp[i] = (temp[i] + 1) % (temp.Count() + 1);
                    result.Add(GetValue(temp));
                }
            }

            if (result.Count() == 1)
                Visited = true;

            return result;
        }
    }
}
