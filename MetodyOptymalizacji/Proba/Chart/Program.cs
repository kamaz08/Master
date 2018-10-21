using Proba.Graph;
using Proba.L1;
using System;
using System.Drawing;
using System.Linq;

namespace Proba
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var result = new Tests().StartTest(100, 10, 10000, 10);

            var graph = new GraphGenerator();
            graph.AddSeries("quick", System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line, result.Item1.Select(x => (double) x.Key).ToList(), result.Item1.Select(x => x.Value.Item1).ToList(), Color.Red);
            graph.AddSeries("sedge", System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line, result.Item2.Select(x => (double) x.Key).ToList(), result.Item2.Select(x => x.Value.Item1).ToList(), Color.Blue);
            graph.AddSeries("yaro",  System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line, result.Item3.Select(x => (double) x.Key).ToList(), result.Item3.Select(x => x.Value.Item1).ToList(), Color.Green);
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
    }
}
