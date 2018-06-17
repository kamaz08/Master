using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L4.Algo
{
    public class CostElement
    {
        public int Id { get; set; }
        public int Cost { get; set; }
        public int ParentId { get; set; }
    }



    public class OfflinePageMigration : AbstractPageMigration
    {
        private List<List<CostElement>> _costList;

        public OfflinePageMigration(int current, ICost cost) : this(current, 32, 64, cost) { }

        public OfflinePageMigration(int current, int changeBaseCost, int size, ICost cost) : base(current, changeBaseCost, size, cost)
        {
            _costList = new List<List<CostElement>>();
            _costList.Add(new List<CostElement> { new CostElement { Cost = 0, Id = current, ParentId = 0 } });
        }

        public override int Calculate(List<int> listOfMigration)
        {
            for(int i = 0; i< listOfMigration.Count; i++)
            {
                var element = listOfMigration[i];
                _costList.Add(new List<CostElement>());
                Enumerable.Range(0, 64).ToList().ForEach(x =>
                {
                    var parent = _costList[i]
                        .OrderBy(y => y.Cost + _changeBaseCost * _cost.GetCost(x, y.Id) + _cost.GetCost(x, element))
                        .First();

                    _costList[i+1].Add(new CostElement
                    {
                        Id = x,
                        ParentId = parent.Id,
                        Cost = parent.Cost + _changeBaseCost * _cost.GetCost(x, parent.Id) + _cost.GetCost(x, element)
                    });
                });
            }

            return _costList.Last().Min(x => x.Cost);
        }
    }
}
