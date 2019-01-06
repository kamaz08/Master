using System.Collections.Generic;
using System.Linq;

namespace Chart.L3
{
    public class Result
    {
        public double ConstPointNumber { get; set; }
        public double CyclesNumber { get; set; }
        public double RecordNumber { get; set; }
    }


    public interface IPermutationChecker
    {
        Result GetResult(IList<int> permutation);
    }

    public class PermutationChecker : IPermutationChecker
    {
        public Result GetResult(IList<int> permutation)
        {
            return new Result
            {
                ConstPointNumber = GetConstPoint(permutation),
                CyclesNumber = GetCyclesNumber(permutation),
                RecordNumber = GetRecordNumber(permutation)
            };
        }

        private int GetRecordNumber(IList<int> permutation)
        {
            var result = 0;
            var max = permutation[0];
            for (int i = 1; i < permutation.Count; i++)
            {
                if (max > permutation[i]) continue;
                max = permutation[i];
                result++;
            }

            return result;
        }

        private int GetCyclesNumber(IList<int> permutation)
        {
            var result = 0;
            var visitedList = Enumerable.Range(0, permutation.Count).Select(x => false).ToList();

            for (int i = 0; i < permutation.Count; i++)
            {
                if (visitedList[i] || permutation[i] == i ) continue;
                result++;
                var iter = i;
                while (visitedList[permutation[iter]] == false)
                {
                    visitedList[permutation[iter]] = true;
                    iter = permutation[iter];
                }
            }
            return result;
        }

        private int GetConstPoint(IList<int> permutation)
        {
            var result = 0;
            for (int i = 0; i < permutation.Count; i++)
            {
                if (permutation[i] == i) result++;
            }
            return result;
        }
    }
}
