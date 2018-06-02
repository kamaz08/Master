using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L4.Algo
{
    public class RandomPageMigration : AbstractPageMigration
    {
        private Random _random;
        public RandomPageMigration(ICost cost) : this(0, 32, 64, cost, new Random()) { }

        public RandomPageMigration(int current, int changeBaseCost, int size, ICost cost, Random random) : base(current, changeBaseCost, size, cost)
        {
            _random = random;
        }

        public override int Calculate(List<int> listOfMigration)
        {
            var result = 0;
            listOfMigration.ForEach(x =>
            {
                result += _cost.GetCost(_current, x);
                if(_random.NextDouble() < 1.0 / (2.0 * _changeBaseCost))
                {
                    result += _changeBaseCost * _cost.GetCost(_current, x);
                    _current = x;
                }
            });
            return result;
        }
    }
}
