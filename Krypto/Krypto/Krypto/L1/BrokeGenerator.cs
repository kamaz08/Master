using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Krypto.L1
{
    public interface IBrokeGenerator
    {
        void Broke(IRandomGenerator generator);
    }
    public class LcgBrokeGenerator : IBrokeGenerator
    {
        public void Broke(IRandomGenerator generator)
        {
            var x0 = generator.Random();
            var x1 = generator.Random();
            var x2 = generator.Random();

            uint a = (x2 - x1) / ( x1 - x0);
            uint c = x1 - a * x0;
            uint b = (x0 - c) / a;
        }
    }
}