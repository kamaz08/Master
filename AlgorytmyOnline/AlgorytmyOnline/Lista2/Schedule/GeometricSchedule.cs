using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lista2.Schedule
{
    public class GeometricSchedule : ScheduleAbstract
    {
        private Random _random;
        public GeometricSchedule(int n): this(n, new Random()) { }
        public GeometricSchedule(int n, Random random) : base(n)
        {
            _random = random;
        }

        public override int GetNextNumber()
        {
            var x = _random.NextDouble();
            for (int i = 1; i < _n; i++)
                if (x >= 1.0 / Math.Pow(2, i)) return i;
            return _n;
        }
    }
}
