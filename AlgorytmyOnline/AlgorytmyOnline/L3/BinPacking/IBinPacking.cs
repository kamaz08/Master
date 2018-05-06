using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L3.BinPacking
{
    public interface IBinPacking
    {
        int Pack(double binSize, List<double> elements);
    }

    public class DoubleElement
    {
        public double Value { get; set; }
    }
}
