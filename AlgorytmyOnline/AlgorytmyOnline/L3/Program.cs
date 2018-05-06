using L3.BinPacking;
using L3.BinRandomNamespace;
using Lista2.Graph;
using Lista2.Schedule;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L3
{
    class Program
    {
        static void Main(string[] args)
        {
            Test();
            Test2();
        }

        static void Test()
        {
            int n = 100, k = 10;
            double binSize = 1.0;
            int testNumber = 1000;
            var binsAlgo = new List<IBinPacking>
            {
                new NextFit(),
                new RandomFit(),
                new FirstFit(),
                new BestFit(),
                new WorstFit()
            };

            var randomBin = new List<IBinRandom>
            {
                new BinRandom(new LinearSchedule(k)),
                new BinRandom(new HarmonicSchedule(k)),
                new BinRandom(new DoubleHarmonicSchedule(k)),
                new BinRandom(new GeometricSchedule(k))
            };

            var result = new List<List<List<int>>>
            {
                new List<List<int>>{new List<int>(), new List<int>(), new List<int>(), new List<int>()},
                new List<List<int>>{new List<int>(), new List<int>(), new List<int>(), new List<int>()},
                new List<List<int>>{new List<int>(), new List<int>(), new List<int>(), new List<int>()},
                new List<List<int>>{new List<int>(), new List<int>(), new List<int>(), new List<int>()},
                new List<List<int>>{new List<int>(), new List<int>(), new List<int>(), new List<int>()},
                new List<List<int>>{new List<int>(), new List<int>(), new List<int>(), new List<int>()}
            };

            for (int t = 0; t < testNumber; t++)
                for (int i = 0; i < randomBin.Count(); i++)
                {
                    var elements = randomBin[0].GetElements(n);
                    for (int j = 0; j < binsAlgo.Count(); j++)
                        result[j][i].Add(binsAlgo[j].Pack(binSize, elements));
                    result[5][i].Add((int)Math.Ceiling(elements.Sum()));
                }

            var graph = new GraphGenerator(1000, 600);
            var kk = new List<double> { 0, 1, 2, 3 };

            var opt = result[5].Select(x => x.Average()).ToList();
            var v0 = new List<List<double>>
            {
                GetAverate(result[0].Select(x => x.Average()).ToList(), opt),
                GetAverate(result[1].Select(x => x.Average()).ToList(), opt),
                GetAverate(result[2].Select(x => x.Average()).ToList(), opt),
                GetAverate(result[3].Select(x => x.Average()).ToList(), opt),
                GetAverate(result[4].Select(x => x.Average()).ToList(), opt)
            };


            graph.AddSeries($"Next", System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column, kk, v0[0], Color.Red);
            graph.AddSeries($"Random", System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column, kk, v0[1], Color.Blue);
            graph.AddSeries($"First", System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column, kk,v0[2], Color.Black);
            graph.AddSeries($"Best", System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column, kk, v0[3], Color.Yellow);
            graph.AddSeries($"Worst", System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column, kk,v0[4], Color.Green);
            //graph.AddSeries($"Hue", System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column, kk, result[5].Select(x => x.Average()).ToList(), Color.MediumPurple);

            graph.SaveGraph("_BinPackingg");
        }

        static void Test2()
        {
            int n = 100, k = 10;
            double binSize = 1.0;
            int testNumber = 1000;
            var binsAlgo = new List<IBinPacking>
            {
                new NextFit(),
                new RandomFit(),
                new FirstFit(),
                new BestFit(),
                new WorstFit()
            };

            var randomBin = new List<IBinRandom>
            {
                new MyBinRandom(new LinearSchedule(k)),
                new MyBinRandom(new HarmonicSchedule(k)),
                new MyBinRandom(new DoubleHarmonicSchedule(k)),
                new MyBinRandom(new GeometricSchedule(k))
            };

            var result = new List<List<List<int>>>
            {
                new List<List<int>>{new List<int>(), new List<int>(), new List<int>(), new List<int>()},
                new List<List<int>>{new List<int>(), new List<int>(), new List<int>(), new List<int>()},
                new List<List<int>>{new List<int>(), new List<int>(), new List<int>(), new List<int>()},
                new List<List<int>>{new List<int>(), new List<int>(), new List<int>(), new List<int>()},
                new List<List<int>>{new List<int>(), new List<int>(), new List<int>(), new List<int>()},
                new List<List<int>>{new List<int>(), new List<int>(), new List<int>(), new List<int>()}
            };

            for (int t = 0; t < testNumber; t++)
                for (int i = 0; i < randomBin.Count(); i++)
                {
                    var elements = randomBin[0].GetElements(n);
                    for (int j = 0; j < binsAlgo.Count(); j++)
                        result[j][i].Add(binsAlgo[j].Pack(binSize, elements));
                    result[5][i].Add((int)Math.Ceiling(elements.Sum()));
                }

            var graph = new GraphGenerator(1000, 600);
            var kk = new List<double> { 0, 1, 2, 3 };

            var opt = result[5].Select(x => x.Average()).ToList();
            var v0 = new List<List<double>>
            {
                GetAverate(result[0].Select(x => x.Average()).ToList(), opt),
                GetAverate(result[1].Select(x => x.Average()).ToList(), opt),
                GetAverate(result[2].Select(x => x.Average()).ToList(), opt),
                GetAverate(result[3].Select(x => x.Average()).ToList(), opt),
                GetAverate(result[4].Select(x => x.Average()).ToList(), opt)
            };



            graph.AddSeries($"Next", System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column, kk, v0[0], Color.Red);
            graph.AddSeries($"Random", System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column, kk, v0[1], Color.Blue);
            graph.AddSeries($"First", System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column, kk, v0[2], Color.Black);
            graph.AddSeries($"Best", System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column, kk, v0[3], Color.Yellow);
            graph.AddSeries($"Worst", System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column, kk, v0[4], Color.Green);
            graph.SaveGraph("_BinPackingg2");
        }

        public static List<double> GetAverate(List<double> l, List<double> a)
        {
            var result = new List<double>();
            for (int i = 0; i < l.Count(); i++)
                result.Add(l[i] / a[i]);
            return result;
        }
    }




}
