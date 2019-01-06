using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chart.L4
{
    public class MeanList
    {
        private double _average;
        private double _diffrent;
        private Coin _coin;

        public MeanList(double propdiffrent) : this(new Coin(new Random(), propdiffrent), 2, 100)
        {
        }

        public MeanList(Coin coin, double average, double diffrent)
        {
            _average = average;
            _diffrent = diffrent;
            _coin = coin;

        }

        public IList<double> GetList(int n)
        {
            return Enumerable.Range(0, n).Select(x => _coin.ThrowCoin() ? _average : _diffrent).ToList();
        }
    }
}
