using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L3.BinPacking
{
    public class NextFit : IBinPacking
    {
        public int Pack(double binSize, List<double> elements)
        {
            double temp = 0;
            int count = 1;

            elements.ForEach(el =>
            {
                if (temp + el <= binSize)
                    temp += el;
                else
                {
                    temp = el;
                    count++;
                }
            });
            return count;
        }
    }
}
