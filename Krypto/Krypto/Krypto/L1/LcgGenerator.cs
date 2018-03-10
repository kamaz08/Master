using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Krypto.L1
{
    public interface IRandomGenerator
    {
        uint Random();
    }
    public class LcgGenerator : IRandomGenerator
    {
        private uint _a, _b, _c;
        public LcgGenerator(uint a, uint b, uint c) 
        {
            _a = a;
            _b = b;
            _c = c;
        }

        public uint Random()
        {
            var result = (_a * _b + _c);
            _b = result;
            return result;
        }
    }
}