using Lista2.Cache;
using Lista2.Graph;
using Lista2.Schedule;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lista2
{
    /*Czerwony fifo
     *Niebieski FWF
     *Czarny LRU 
     *Żółty LFU
     *Zielony RAND
     *Fioletowy RMA
     */


    class Program
    {
        static void Main(string[] args)
        {
            new Program().Test(1000);

        }


        void Test(int t)
        {
            var Listn = new List<int> { 20, 30, 40, 50, 60, 70, 80, 90, 100 };
            Listn.ForEach(n =>
            {
                Test(t, n, new LinearSchedule(n), "Linear ");
                Test(t, n, new HarmonicSchedule(n), "Harmonic ");
                Test(t, n, new DoubleHarmonicSchedule(n), "Double Harmonic ");
                Test(t, n, new GeometricSchedule(n), "Geometric ");
            });
        }

        void Test(int t, int n, ISchedule schedule, string scheduleName)
        {
            var graph = new GraphGenerator(1000,600);
            var Listk = new List<int> { 10, 9, 8, 7, 6, 5 };

            var fifol = new List<double>();
            var fwfl = new List<double>();
            var lrul = new List<double>();
            var lful = new List<double>();
            var randl = new List<double>();
            var rmal = new List<double>();

            Listk.ForEach(k =>
            {
                var fifo = new FIFOCache(n / k);
                var fwf = new FWFCachecs(n / k);
                var lru = new LRUCache(n / k);
                var lfu = new LFUCache(n / k);
                var rand = new RandCache(n / k);
                var rma = new RMACache(n / k);

                int fifoc, fwfc, lruc, lfuc, randc, rmac;
                fifoc = fwfc = lruc = lfuc = randc = rmac = 0;

                for (int j = 0; j < 100; j++)
                {
                    for (int i = 0; i < t; i++)
                    {
                        var x = schedule.GetNextNumber();
                        fifoc += fifo.GetValue(x);
                        fwfc += fwf.GetValue(x);
                        lruc += lru.GetValue(x);
                        lfuc += lfu.GetValue(x);
                        randc += rand.GetValue(x);
                        rmac += rma.GetValue(x);
                    }
                }
                fifol.Add(fifoc / 100);
                fwfl.Add(fwfc / 100);
                lrul.Add(lruc / 100);
                lful.Add(lfuc / 100);
                randl.Add(randc / 100);
                rmal.Add(rmac / 100);
            });
            var kk = Listk.Select(x =>(double) (n / x)).ToList();
            graph.AddSeries($"{scheduleName}_FIFO_", System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line, kk, fifol, Color.Red);
            graph.AddSeries($"{scheduleName}_FWF_", System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line, kk, fwfl, Color.Blue);
            graph.AddSeries($"{scheduleName}_LRU_", System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line, kk, lrul, Color.Black);
            graph.AddSeries($"{scheduleName}_LFU_", System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line, kk, lful, Color.Yellow);
            graph.AddSeries($"{scheduleName}_RAND_", System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line, kk, randl, Color.Green);
            graph.AddSeries($"{scheduleName}_RMA_", System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line, kk, rmal, Color.MediumPurple);

            graph.SaveGraph($"{scheduleName}_{n}_3");
        }
    }
}
