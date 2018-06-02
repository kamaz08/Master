using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L4.Algo
{
    public class MoveToMinPageMigration : AbstractPageMigration
    {
        public MoveToMinPageMigration(ICost cost) : this(0, 32, 64, cost) { }

        public MoveToMinPageMigration(int current, int changeBaseCost, int size, ICost cost) : base(current, changeBaseCost, size, cost) { }

        public override int Calculate(List<int> listOfMigration)
        {
            var list = new List<int>(listOfMigration);
            var result = FindMin(list.Take(_changeBaseCost).ToList(), true);

            list = list.Skip(_changeBaseCost).ToList();

            while(list.Count != 0)
            {
                result += FindMin(list.Take(_changeBaseCost).ToList());
                list = list.Skip(_changeBaseCost).ToList();
            }
            return result;
        }

        private int FindMin(List<int> list, bool isFirst = false)
        {
            Enumerable.Range(0, 64).Min(x => list.Sum(y => _cost.GetCost(x, y)));

            int min = int.MaxValue;
            int minsum = int.MaxValue;
            for (int i = 0; i < 64; i++)
            {
                var tempsum = list.Sum(x => _cost.GetCost(i, x));
                tempsum += i == _current || isFirst ? 0 : (_cost.GetCost(i, _current) * _changeBaseCost);
                if (tempsum < minsum)
                {
                    minsum = tempsum;
                    min = i;
                }
            }
            _current = min;
            return minsum;
        }
    }
}
