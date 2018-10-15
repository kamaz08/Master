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

        public Tuple<Dictionary<int, Tuple<double, double>>, Dictionary<int, Tuple<double, double>>, Dictionary<int, Tuple<double, double>>> StartTest(int testNumber, int from, int to, int step)
        {
            var dictquick = new Dictionary<int, Tuple<double, double>>();
            var dictSedge = new Dictionary<int, Tuple<double, double>>();
            var dictyaro = new Dictionary<int, Tuple<double, double>>();

            var quick = new QuickSort(new[] { 1 });
            var sedge = new DualPivotQuicksortSedgewick(new[] { 1 });
            var yaro = new DualPivotQuicksortYaroslavskiy(new[] { 1 });

            Enumerable.Range(0, (to - from) / step + 1).ToList().ForEach(_arrLen =>
            {
                var arrayLength = from + _arrLen * step;

                var quicktemp = new List<SortStatistic>();
                var sedgetemp = new List<SortStatistic>();
                var yarotemp  = new List<SortStatistic>();

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

                dictquick.Add(arrayLength, Tuple.Create(quicktemp.Average(x => x.NoCheck), quicktemp.Average(x => x.NoSwap)));
                dictSedge.Add(arrayLength, Tuple.Create(sedgetemp.Average(x => x.NoCheck), sedgetemp.Average(x => x.NoSwap)));
                dictyaro.Add(arrayLength, Tuple.Create(yarotemp.Average(x => x.NoCheck), yarotemp.Average(x => x.NoSwap)));
            });
            return Tuple.Create(dictquick, dictSedge, dictyaro);
        }
    }
}
