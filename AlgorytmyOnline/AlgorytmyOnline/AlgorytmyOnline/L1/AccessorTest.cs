using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AlgorytmyOnline.L1
{
    public interface IAccessorTest
    {
        int Test(int number, int count);
    }
    public class AccessorTest : IAccessorTest
    {
        private ISelfAssembler<CountItem> _selfAssembler;
        private NumberPropability _numberPropability;

        public AccessorTest(ISelfAssembler<CountItem> selfAssembler, NumberPropability numberPropability)
        {
            _selfAssembler = selfAssembler;
            _numberPropability = numberPropability;
        }

        public int Test(int number, int count)
        {
            int result = 0;
            for (int j = 0; j < count; j++)
            {
                var list = new List<CountItem>();

                for (int i = 0; i < number; i++)
                {
                    var num = _numberPropability.GetNextInt();
                    var index = 0;
                    var item = list.FirstOrDefault(x =>
                    {
                        index++;
                        result++;
                        return x.Value == num;
                    });

                    if (item == null)
                        list.Add(new CountItem(num));
                    else
                        index--;

                    list = _selfAssembler.Assmebly(list.ToArray(), index).ToList();
                }
            }
            return result / count;
        }
    }
}