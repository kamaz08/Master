using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AlgorytmyOnline.L1
{
    public class GeometricPropability : NumberPropability
    {
        public GeometricPropability() : base() { }

        public override int GetNextInt()
        {
            var result = (int)Math.Ceiling(-Math.Log(_random.NextDouble()) / Math.Log(2.0));
            result = result > 100 ? 100 : result;
            return result;
        }
    }
}