using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L4.Algo
{
    public abstract class AbstractPageMigration
    {
        protected int _current;
        protected int _changeBaseCost;
        protected int _size;
        protected ICost _cost;

        public AbstractPageMigration(ICost Cost) :this(0, 32, 64, Cost) { }
        public AbstractPageMigration(int current, int changeBaseCost, int size, ICost cost)
        {
            _current = current;
            _changeBaseCost = changeBaseCost;
            _size = size;
            _cost = cost;
        }
        public abstract int Calculate(List<int> listOfMigration);
    }
}
