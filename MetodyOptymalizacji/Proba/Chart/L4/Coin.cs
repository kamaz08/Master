using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chart.L4
{
    public class Coin
    {
        private Random _random;
        private double _prop;
        public Coin() : this(new Random(), 0.5) { }

        public Coin(Random random, double successProp)
        {
            _random = random;
            _prop = successProp;
        }

        public bool ThrowCoin()
        {
            return _random.NextDouble() <= _prop;
        }
    }
}
