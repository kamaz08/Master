using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L3.BinPacking
{
    public class RandomFit: IBinPacking
    {
        private Random _random;
        public RandomFit() : this(new Random()) { }
        public RandomFit(Random random)
        {
            _random = random;
        }

        public int Pack(double binSize, List<double> elements)
        {

            var binList = new List<DoubleElement>();
            binList.Add(new DoubleElement());

            elements.ForEach(el =>
            {
                var binTemp = binList.Where(bin => bin.Value + el <= binSize).ToList();
                if (binTemp.Any())
                    binTemp[_random.Next(binTemp.Count())].Value += el;
                else
                    binList.Add(new DoubleElement { Value = el });
            });
            return binList.Count();
        }
    }
}
