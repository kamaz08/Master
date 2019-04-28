using Proba.Graph;
using Proba.L1;
using System;
using System.Drawing;
using System.Linq;

namespace Proba
{
    class Program
    {
        public static void zad1()
        {
            var result = new Tests().StartTest(100, 10, 10000, 10);

            var graph = new GraphGenerator();
            graph.AddSeries("quick", System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line, result.Item1.Select(x => (double)x.Key).ToList(), result.Item1.Select(x => x.Value.Item1).ToList(), Color.Red);
            graph.AddSeries("sedge", System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line, result.Item2.Select(x => (double)x.Key).ToList(), result.Item2.Select(x => x.Value.Item1).ToList(), Color.Blue);
            graph.AddSeries("yaro", System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line, result.Item3.Select(x => (double)x.Key).ToList(), result.Item3.Select(x => x.Value.Item1).ToList(), Color.Green);
            graph.SaveGraph("oczekiwana check");

            graph = new GraphGenerator();
            graph.AddSeries("quick", System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line, result.Item1.Select(x => (double)x.Key).ToList(), result.Item1.Select(x => x.Value.Item2).ToList(), Color.Red);
            graph.AddSeries("sedge", System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line, result.Item2.Select(x => (double)x.Key).ToList(), result.Item2.Select(x => x.Value.Item2).ToList(), Color.Blue);
            graph.AddSeries("yaro", System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line, result.Item3.Select(x => (double)x.Key).ToList(), result.Item3.Select(x => x.Value.Item2).ToList(), Color.Green);
            graph.SaveGraph("wariacja check");

            graph = new GraphGenerator();
            graph.AddSeries("quick", System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line, result.Item1.Select(x => (double)x.Key).ToList(), result.Item1.Select(x => x.Value.Item3).ToList(), Color.Red);
            graph.AddSeries("sedge", System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line, result.Item2.Select(x => (double)x.Key).ToList(), result.Item2.Select(x => x.Value.Item3).ToList(), Color.Blue);
            graph.AddSeries("yaro", System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line, result.Item3.Select(x => (double)x.Key).ToList(), result.Item3.Select(x => x.Value.Item3).ToList(), Color.Green);
            graph.SaveGraph("oczekiwana swap");


            graph = new GraphGenerator();
            graph.AddSeries("quick", System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line, result.Item1.Select(x => (double)x.Key).ToList(), result.Item1.Select(x => x.Value.Item4).ToList(), Color.Red);
            graph.AddSeries("sedge", System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line, result.Item2.Select(x => (double)x.Key).ToList(), result.Item2.Select(x => x.Value.Item4).ToList(), Color.Blue);
            graph.AddSeries("yaro", System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line, result.Item3.Select(x => (double)x.Key).ToList(), result.Item3.Select(x => x.Value.Item4).ToList(), Color.Green);
            graph.SaveGraph("wariacja swap");
        }

        public static void zad2()
        {
            new Chart.L2.Tests().Test();    
        }

        public static void L3()
        {
            Chart.L3.Test.L3(100, 10, 10000);
        }

        public static void L4()
        {
             Chart.L4.Tests.GamePlay(1000);

            Chart.L4.Tests.Test(70,2,1000);
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            //zad2();

            //L3();

            L4();


        }
    }
}
