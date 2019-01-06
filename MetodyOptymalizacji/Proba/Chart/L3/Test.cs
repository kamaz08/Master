using Proba.Graph;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Chart.L3
{
    public class TestResult
    {
        public double n { get; set; }
        public Result ExpectedValue { get; set; }
        public Result Variance { get; set; }
        public Result Kurtosis { get; set; }
    }

    public class Test
    {
        public static void L3(int count, int step, int testCount)
        {
            var test = new Test();

            test.Testing(count, step, testCount);
        }


        public void Testing(int count, int step, int testCount)
        {
            var result = Enumerable.Range(1, count).Select(x => GetResult(x * step, testCount)).ToList();

            var numbers = result.Select(x => x.n).ToList();

            var graph = new GraphGenerator();
            graph.AddSeries("constPoint", System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line, numbers, result.Select(x => x.ExpectedValue.ConstPointNumber).ToList(),  Color.Red);
            graph.AddSeries("Cycles", System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line, numbers, result.Select(x => x.ExpectedValue.CyclesNumber).ToList(), Color.Blue);
            graph.AddSeries("Records", System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line, numbers, result.Select(x => x.ExpectedValue.RecordNumber).ToList(), Color.Green);
            graph.SaveGraph("ExpectedValue " + DateTime.Now);

            graph = new GraphGenerator();
            graph.AddSeries("constPoint", System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line, numbers, result.Select(x => x.Variance.ConstPointNumber).ToList(), Color.Red);
            graph.AddSeries("Cycles", System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line, numbers, result.Select(x => x.Variance.CyclesNumber).ToList(), Color.Blue);
            graph.AddSeries("Records", System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line, numbers, result.Select(x => x.Variance.RecordNumber).ToList(), Color.Green);
            graph.SaveGraph("Variance " + DateTime.Now);

            graph = new GraphGenerator();
            graph.AddSeries("constPoint", System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line, numbers, result.Select(x => x.Kurtosis.ConstPointNumber).ToList(), Color.Red);
            graph.AddSeries("Cycles", System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line, numbers, result.Select(x => x.Kurtosis.CyclesNumber).ToList(), Color.Blue);
            graph.AddSeries("Records", System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line, numbers, result.Select(x => x.Kurtosis.RecordNumber).ToList(), Color.Green);
            graph.SaveGraph("Kurtosis " + DateTime.Now);
        }


        public TestResult GetResult(int n, int testCount)
        {
            var a = new Permutation(n);
            var checker = new PermutationChecker();

            var results = new List<Result>();

            Enumerable.Range(1, testCount).ToList().ForEach(x =>
             {
                 a.Reset();
                 var per = a.GetPermutation();

                 results.Add(checker.GetResult(per));
             });

            var resE = new Result
            {
                ConstPointNumber = results.Select(x => x.ConstPointNumber).Average(),
                CyclesNumber = results.Select(x => x.CyclesNumber).Average(),
                RecordNumber = results.Select(x => x.RecordNumber).Average()
            };

            var tn = (double)testCount;

            var mul = (tn / (tn - 1));

            var resV = new Result
            {
                ConstPointNumber = mul * results.Select(x => x.ConstPointNumber).Average(x => Math.Pow(x - resE.ConstPointNumber, 2)),
                CyclesNumber = mul * results.Select(x => x.CyclesNumber).Average(x => Math.Pow(x - resE.CyclesNumber, 2)),
                RecordNumber = mul * results.Select(x => x.RecordNumber).Average(x => Math.Pow(x - resE.RecordNumber, 2))
            };


            var multiply = ((tn * (tn + 1)) / ((tn - 1) * (tn - 2) * (tn - 3)));
            var dec = ((3 * (tn - 1) * (tn - 1)) / ((tn - 2) * (tn - 3)));

            var resK = new Result
            {
                ConstPointNumber = multiply * results.Select(x => x.ConstPointNumber).Sum(x => Math.Pow(((x - resE.ConstPointNumber) / (Math.Sqrt(resV.ConstPointNumber))), 4)) - dec,
                CyclesNumber = multiply * results.Select(x => x.CyclesNumber).Sum(x => Math.Pow(((x - resE.CyclesNumber) / (Math.Sqrt(resV.CyclesNumber))), 4)) - dec,
                RecordNumber = multiply * results.Select(x => x.RecordNumber).Sum(x => Math.Pow(((x - resE.RecordNumber) / (Math.Sqrt(resV.RecordNumber))), 4)) - dec
            };

            var result = new TestResult
            {
                n = n,
                ExpectedValue = resE,
                Variance = resV,
                Kurtosis = resK
            };

            return result;
        }
    }
}
