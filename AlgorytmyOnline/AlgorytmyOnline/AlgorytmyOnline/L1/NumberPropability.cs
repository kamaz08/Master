using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorytmyOnline.L1
{
    public abstract class NumberPropability
    {
        protected Random _random { get; set; }

        public NumberPropability() : this(new Random()) { }
        public NumberPropability(Random random)
        {
            _random = random;
        }
        public abstract int GetNextInt();
    }
}
