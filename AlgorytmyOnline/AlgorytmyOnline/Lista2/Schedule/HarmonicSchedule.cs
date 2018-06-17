using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lista2.Schedule
{
    public class HarmonicSchedule : ScheduleAbstract
    {
        private Random _random;
        private double _hr;

        public HarmonicSchedule(int n) : this(n, new Random())
        {
        }
        public HarmonicSchedule(int n, Random random): base(n)
        {
            _random = random;
            _hr = Enumerable.Range(1, n).Sum(x => 1.0 / x);
        }

        public override int GetNextNumber()
        {
            var x = _random.NextDouble();
            double sum = 0;
            for (int i = 1; i <= _n; i++)
            {
                sum += 1.0 / (_hr * i);
                if (x <= sum) return i;

            }
            return _n;
        }
    }
}
