using Lista2.Schedule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L3.BinRandomNamespace
{
    public interface IBinRandom
    {
        List<double> GetElements(int n);
    }
    public class BinRandom : IBinRandom
    {
        private ISchedule _schedule;
        private Random _random;

        public BinRandom(ISchedule schedule) : this(schedule, new Random()) { }
        public BinRandom(ISchedule schedule, Random random)
        {
            _schedule = schedule;
            _random = random;
        }

        public List<double> GetElements(int n)
        {
            var result = new List<double>();

            while (result.Count() < n)
            {
                var item = _random.NextDouble() * 0.7;
                var itemCount = _schedule.GetNextNumber();

                for (int i = 0; i < itemCount; i++)
                    result.Add(item);

            }
            return result.Take(n).ToList();
        }

    }

    public class MyBinRandom : IBinRandom
    {
        private ISchedule _schedule;

        public MyBinRandom(ISchedule schedule)
        {
            _schedule = schedule;
        }

        public List<double> GetElements(int n)
        {
            var result = new List<double>();

            for (int i = 0; i < n; i++)
                result.Add(_schedule.GetNextNumber() / 10.0);
            return result;
        }
    }
}
