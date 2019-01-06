using Proba.Graph;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chart.L2
{
    public class Tests
    {
        public void Test()
        {/*
            var result = Enumerable.Range(1, 100).Select(x =>
            {
                var g = new Chart.L2.Graph(x*10, 1.0 / (2.0 * x*10));
                var a = Enumerable.Range(0, 100).Select(y =>
                {
                    g.Generate();
                    return g.GetResult();
                });
                return new Chart.L2.Result
                {
                    Size = x*10,
                    First = a.Average(z => z.First),
                    Second = a.Average(z => z.Second)
                };
            }).ToList();
            
            var graph = new GraphGenerator();
            graph.AddSeries("first", System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line, result.Select(x=>(double)x.Size).ToList(), result.Select(x => x.First).ToList(), Color.Red);
            graph.AddSeries("second", System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line, result.Select(x => (double)x.Size).ToList(), result.Select(x => x.Second).ToList(), Color.Blue);
            graph.SaveGraph("a"+Guid.NewGuid().ToString());
            */
            var result = Enumerable.Range(1, 100).Select(x =>
            {
                var g = new Chart.L2.Graph(x * 10, (1.0 / (x*10)) - (Math.Pow(x*10.0, 0.1) / (Math.Pow(x * 10.0, (4.0/3.0)))));
                var a = Enumerable.Range(0, 100).Select(y =>
                {
                    g.Generate();
                    return g.GetResult();
                });
                return new Chart.L2.Result
                {
                    Size = x * 10,
                    First = a.Average(z => z.First),
                    Second = a.Average(z => z.Second)
                };
            }).ToList();

            var graph = new GraphGenerator();
            graph.AddSeries("first", System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line, result.Select(x => (double)x.Size).ToList(), result.Select(x => x.First).ToList(), Color.Red);
            graph.AddSeries("second", System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line, result.Select(x => (double)x.Size).ToList(), result.Select(x => x.Second).ToList(), Color.Blue);
            graph.SaveGraph("b" + Guid.NewGuid().ToString());

            result = Enumerable.Range(1, 100).Select(x =>
            {
                var g = new Chart.L2.Graph(x * 10, (1.0 / (x * 10)) - (2.0 / (Math.Pow(x * 10.0, (4.0 / 3.0)))));
                var a = Enumerable.Range(0, 100).Select(y =>
                {
                    g.Generate();
                    return g.GetResult();
                });
                return new Chart.L2.Result
                {
                    Size = x * 10,
                    First = a.Average(z => z.First),
                    Second = a.Average(z => z.Second)
                };
            }).ToList();

            graph = new GraphGenerator();
            graph.AddSeries("first", System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line, result.Select(x => (double)x.Size).ToList(), result.Select(x => x.First).ToList(), Color.Red);
            graph.AddSeries("second", System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line, result.Select(x => (double)x.Size).ToList(), result.Select(x => x.Second).ToList(), Color.Blue);
            graph.SaveGraph("c" + Guid.NewGuid().ToString());

            result = Enumerable.Range(1, 100).Select(x =>
            {
                var g = new Chart.L2.Graph(x * 10, (1.0 / (x * 10)) + (2 / (Math.Pow(x * 10.0, (4.0 / 3.0)))));
                var a = Enumerable.Range(0, 100).Select(y =>
                {
                    g.Generate();
                    return g.GetResult();
                });
                return new Chart.L2.Result
                {
                    Size = x * 10,
                    First = a.Average(z => z.First),
                    Second = a.Average(z => z.Second)
                };
            }).ToList();

            graph = new GraphGenerator();
            graph.AddSeries("first", System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line, result.Select(x => (double)x.Size).ToList(), result.Select(x => x.First).ToList(), Color.Red);
            graph.AddSeries("second", System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line, result.Select(x => (double)x.Size).ToList(), result.Select(x => x.Second).ToList(), Color.Blue);
            graph.SaveGraph("d" + Guid.NewGuid().ToString());

            result = Enumerable.Range(1, 100).Select(x =>
            {
                var g = new Chart.L2.Graph(x * 10, (1.0 / (x * 10)) + (Math.Pow(x * 10.0, 0.1) / (Math.Pow(x * 10.0, (4.0 / 3.0)))));
                var a = Enumerable.Range(0, 100).Select(y =>
                {
                    g.Generate();
                    return g.GetResult();
                });
                return new Chart.L2.Result
                {
                    Size = x * 10,
                    First = a.Average(z => z.First),
                    Second = a.Average(z => z.Second)
                };
            }).ToList();

            graph = new GraphGenerator();
            graph.AddSeries("first", System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line, result.Select(x => (double)x.Size).ToList(), result.Select(x => x.First).ToList(), Color.Red);
            graph.AddSeries("second", System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line, result.Select(x => (double)x.Size).ToList(), result.Select(x => x.Second).ToList(), Color.Blue);
            graph.SaveGraph("e" + Guid.NewGuid().ToString());

            result = Enumerable.Range(1, 100).Select(x =>
            {
                var g = new Chart.L2.Graph(x * 10, 2.0 / (x * 10));
                var a = Enumerable.Range(0, 100).Select(y =>
                {
                    g.Generate();
                    return g.GetResult();
                });
                return new Chart.L2.Result
                {
                    Size = x * 10,
                    First = a.Average(z => z.First),
                    Second = a.Average(z => z.Second)
                };
            }).ToList();

            graph = new GraphGenerator();
            graph.AddSeries("first", System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line, result.Select(x => (double)x.Size).ToList(), result.Select(x => x.First).ToList(), Color.Red);
            graph.AddSeries("second", System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line, result.Select(x => (double)x.Size).ToList(), result.Select(x => x.Second).ToList(), Color.Blue);
            graph.SaveGraph("f" + Guid.NewGuid().ToString());
        }



    }
}
