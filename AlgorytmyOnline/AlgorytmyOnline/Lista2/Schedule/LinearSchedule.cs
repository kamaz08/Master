using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lista2.Schedule
{
    public class LinearSchedule : ScheduleAbstract
    {
        private Random _random;
        public LinearSchedule(int n) : this(n, new Random())
        {
        }
        public LinearSchedule(int n, Random random): base(n)
        {
            _random = random;
        }

        public override int GetNextNumber()
        {
            return _random.Next(_n) + 1;
        }
    }
}
