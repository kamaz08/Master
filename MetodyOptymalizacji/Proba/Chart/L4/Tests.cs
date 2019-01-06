using Proba.Graph;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;


namespace Chart.L4
{
    public class Result
    {
        public double Harmonic { get; set; }
        public double Arytmetic { get; set; }
        public double Geometric { get; set; }
    }

    public class TestResult
    {
        public double n { get; set; }
        public Result Expected { get; set; }
        public Result Variance { get; set; }
    }

    public class Tests
    {
        public static void GamePlay(int nTest)
        {
            var game1 = new Game(11, 110);
            var game2 = new Game(0.05, 0.5);

            Console.WriteLine($"Gra 11 110 średnia strata przy {nTest} próbach to {Enumerable.Range(0, nTest).Select(x => game1.Play(5)).Average()}");
            Console.WriteLine($"Gra 11 110 średnia strata przy {nTest} próbach to {Enumerable.Range(0, nTest).Select(x => game2.Play(100)).Average()}");
        }

        public static void Test(int count, int step, int testCount)
        {
            var result = Enumerable.Range(1, count).Select(x => GetTestResult(x * step, testCount)).ToList();

            var numbers = result.Select(x => x.n).ToList();

            var graph = new GraphGenerator();
            graph.AddSeries("arytmetyczna ", System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line, numbers, result.Select(x => x.Expected.Arytmetic).ToList(), Color.Red);
            graph.AddSeries("gemetryczna", System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line, numbers, result.Select(x => x.Expected.Geometric).ToList(), Color.Blue);
            graph.AddSeries("harmoniczna", System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line, numbers, result.Select(x => x.Expected.Harmonic).ToList(), Color.Green);
            graph.SaveGraph("ExpectedValue " + DateTime.Now);

            graph = new GraphGenerator();
            graph.AddSeries("arytmetyczna ", System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line, numbers, result.Select(x => x.Variance.Arytmetic).ToList(), Color.Red);
            graph.AddSeries("gemetryczna", System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line, numbers, result.Select(x => x.Variance.Geometric).ToList(), Color.Blue);
            graph.AddSeries("harmoniczna", System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line, numbers, result.Select(x => x.Variance.Harmonic).ToList(), Color.Green);
            graph.SaveGraph("Variance " + DateTime.Now);
        }

        public static TestResult GetTestResult(int n, int testCount)
        {
            var list = Enumerable.Range(0, testCount).Select(x => GetResult(n));

            var result = new TestResult
            {
                n = n,
                Expected = new Result
                {
                    Arytmetic = list.Select(x => x.Arytmetic).Average(),
                    Geometric = list.Select(x => x.Geometric).Average(),
                    Harmonic = list.Select(x => x.Harmonic).Average(),
                }
            };

            var tn = (double)testCount;

            var mult = tn / (tn - 1);
            result.Variance = new Result
            {
                Arytmetic = mult * list.Select(x => x.Arytmetic).Average(x => Math.Pow(x - result.Expected.Arytmetic, 2)),
                Geometric = mult * list.Select(x => x.Geometric).Average(x => Math.Pow(x - result.Expected.Geometric, 2)),
                Harmonic = mult * list.Select(x => x.Harmonic).Average(x => Math.Pow(x - result.Expected.Harmonic, 2))
            };

            return result;
        }

        public static Result GetResult(int n)
        {
            var list = new MeanList(0.05).GetList(n);
            return new Result
            {
                Arytmetic = list.GetArytmeticMean(),
                Harmonic = list.GetHarmonicMean(),
                Geometric = list.GetGeometricMean()
            };
        }

    }
}
