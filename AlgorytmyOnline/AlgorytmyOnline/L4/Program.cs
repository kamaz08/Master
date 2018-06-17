using L4.Algo;
using Lista2.Graph;
using Lista2.Schedule;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L4
{
    class Program
    {
        static void Main(string[] args)
        {
           // GetRandomValues(1024, new DoubleHarmonicSchedule(64));
           // GetRandomValues(1024, new HarmonicSchedule(64));
            Test();



        }



        public static void Test()
        {
            new List<ISchedule>
            {
                new LinearSchedule(64),
                new HarmonicSchedule(64),
                new DoubleHarmonicSchedule(64)
            }.ForEach(random =>
            {
                int mh, mt, rh, rt, offh, offt;
                mh = mt = rh = rt = offh = offt = 0;

                for (int t = 0; t < 10; t++)
                {
                    var start = new Random().Next(64);
                    var test = GetRandomValues(1024, random);
                    var hiper = new HiperKostkaCost();
                    var torus = new TorusCost();
                    mh += new MoveToMinPageMigration(start, hiper).Calculate(test);
                    mt += new MoveToMinPageMigration(start, torus).Calculate(test);
                    rh += new RandomPageMigration(start, hiper).Calculate(test);
                    rt += new RandomPageMigration(start, torus).Calculate(test);
                    offh += new OfflinePageMigration(start, hiper).Calculate(test);
                    offt += new OfflinePageMigration(start, torus).Calculate(test);

                }
                mh /= 10;
                mt /= 10;
                rh /= 10;
                rt /= 10;
                offh /= 10;
                offt /= 10;
                var graph = new GraphGenerator(1000, 600);
                graph.AddSeries("mtmhyper", System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column, new List<double> { 0.0 }, new List<double> { mh }, Color.Green);
                graph.AddSeries("randhyper", System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column, new List<double> { 0.0 }, new List<double> { rh }, Color.Yellow);
                graph.AddSeries("offhyper", System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column, new List<double> { 0.0 }, new List<double> { offh }, Color.Red);

                graph.AddSeries("mtmtorus", System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column, new List<double> { 1.0 }, new List<double> { mt }, Color.Green);
                graph.AddSeries("randtorus", System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column, new List<double> { 1.0 }, new List<double> { rt }, Color.Yellow);
                graph.AddSeries("offtorus", System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column, new List<double> { 1.0 }, new List<double> { offt }, Color.Red);

                graph.SaveGraph(random.GetType().ToString());

            });




        }

        public static List<int> GetRandomValues(int n, ISchedule schedule)
        {
            var result = new List<int>();
            for (int i = 0; i < n; i++)
                result.Add(schedule.GetNextNumber() - 1);
            return result;
        }
    }
}
