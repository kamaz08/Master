using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace L4PageRank
{

    public class Element
    {
        public int Count { get; set; }
        public int LastStep { get; set; }
        public List<int> Steps { get; set; }

        public Element()
        {
            Count = 0;
            LastStep = 0;
            Steps = new List<int>();
        }

        public void Enter(int i)
        {
            Steps.Add(i - LastStep);
            Count++;
            LastStep = i;
        }
    }

    public class ProstaKolejka
    {
        private Random _random;
        public ProstaKolejka():this(new Random()) { }

        public ProstaKolejka(Random random)
        {
            this._random = random;
        }
        public List<Element> Test(double a, double b)
        {
            var que = new List<Element>();
            int state = 0;
            que.Add(new Element());
            for (int i = 1; i < 10000; i++)
            {
                var prop = _random.NextDouble();
                if (prop < a )
                {
                    if (state > 0)
                        state--;
                }
                else if (prop - a < b)
                {
                    state++;
                    if(que.Count <= state)
                        que.Add(new Element());
                }
                que[state].Enter(i);
            }

            return que;
        }
    }
}
