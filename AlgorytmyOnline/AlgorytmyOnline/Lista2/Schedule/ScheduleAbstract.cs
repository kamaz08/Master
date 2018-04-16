using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lista2.Schedule
{
    public abstract class ScheduleAbstract: ISchedule
    {
        protected int _n;
        public ScheduleAbstract(int n)
        {
            _n = n;
        }

        public abstract int GetNextNumber();
    }
}
