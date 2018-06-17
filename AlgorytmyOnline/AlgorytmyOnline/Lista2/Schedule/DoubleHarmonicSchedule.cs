using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lista2.Schedule
{
    public class DoubleHarmonicSchedule : ScheduleAbstract
    {
        private Random _random;
        private double _hr;
        public DoubleHarmonicSchedule(int n) : this(n, new Random()) { }

        public DoubleHarmonicSchedule(int n, Random random) : base(n)
        {
            _random = random;
            _hr = Enumerable.Range(1, n).Sum(x => 1.0 / Math.Pow(x,2.0));
        }

        public override int GetNextNumber()
        {
            var x = _random.NextDouble();
            double sum = 0;
            for (int i = 1; i <= _n; i++)
            {
                sum += 1.0 / (_hr * Math.Pow(i, 2.0));
                if (x <= sum) return i;
            }
            return _n;
        }
    }
}
