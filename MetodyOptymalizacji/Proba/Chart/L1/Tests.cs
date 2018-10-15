using Proba.Random;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Proba.L1
{
    public class Tests
    {
        private IRandomNumber _randomNumber;

        public Tests() : this(new RandomNumber())
        {

        }

        public Tests(IRandomNumber randomNumber)
        {
            _randomNumber = randomNumber;
        }

        public Tuple<Dictionary<int, Tuple<double, double, double, double>>, Dictionary<int, Tuple<double, double, double, double>>, Dictionary<int, Tuple<double, double, double, double>>> StartTest(int testNumber, int from, int to, int step)
        {
            var dictquick = new Dictionary<int, Tuple<double, double, double, double>>();
            var dictSedge = new Dictionary<int, Tuple<double, double, double, double>>();
            var dictyaro = new Dictionary<int, Tuple<double, double, double, double>>();

            var quick = new QuickSort(new[] { 1 });
            var sedge = new DualPivotQuicksortSedgewick(new[] { 1 });
            var yaro = new DualPivotQuicksortYaroslavskiy(new[] { 1 });

            Enumerable.Range(0, (to - from) / step + 1).ToList().ForEach(_arrLen =>
            {
                var arrayLength = from + _arrLen * step;

                var quicktemp = new List<SortStatistic>();
                var sedgetemp = new List<SortStatistic>();
                var yarotemp = new List<SortStatistic>();

                Enumerable.Range(0, testNumber).ToList().ForEach(t =>
                {
                    var table = _randomNumber.GetRandomArray(arrayLength);
                    quick.Reset(table);
                    sedge.Reset(table);
                    yaro.Reset(table);

                    quicktemp.Add(quick.Sort());
                    sedgetemp.Add(sedge.Sort());
                    yarotemp.Add(yaro.Sort());
                });

                var cq = quicktemp.Average(x => x.NoCheck);
                var cs = sedgetemp.Average(x => x.NoCheck);
                var cy = yarotemp.Average(x => x.NoCheck);

                var sq = quicktemp.Average(x => x.NoSwap);
                var ss = sedgetemp.Average(x => x.NoSwap);
                var sy = yarotemp.Average(x => x.NoSwap);


                dictquick.Add(arrayLength, Tuple.Create(cq, 1.0 / (testNumber -1) * quicktemp.Sum(x => Math.Pow(x.NoCheck - cq, 2.0)), sq, 1.0 / (testNumber - 1) * quicktemp.Sum(x => Math.Pow(x.NoSwap - sq, 2.0))));
                dictSedge.Add(arrayLength, Tuple.Create(cs, 1.0 / (testNumber - 1) * sedgetemp.Sum(x => Math.Pow(x.NoCheck - cs, 2.0)), ss, 1.0 / (testNumber - 1) * sedgetemp.Sum(x => Math.Pow(x.NoSwap - ss, 2.0))));
                dictyaro.Add(arrayLength, Tuple.Create(cy, 1.0 / (testNumber - 1) * yarotemp.Sum(x => Math.Pow(x.NoCheck - cy, 2.0)), sy, 1.0 / (testNumber - 1) * yarotemp.Sum(x => Math.Pow(x.NoSwap - sy, 2.0))));
            });
            return Tuple.Create(dictquick, dictSedge, dictyaro);
        }
    }
}
