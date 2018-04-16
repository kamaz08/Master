using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AlgorytmyOnline.L1
{
    public class HarmonicPropability : NumberPropability
    {
        public HarmonicPropability() : base() { }

        public override int GetNextInt()
        {
            var rand = _random.NextDouble();


            return (int) Math.Ceiling(100.5 - Math.Pow(Math.Pow(100.5, 2.0) - 2.0 * (5050 - _random.Next(5050)), 0.5));
        }
    }
}