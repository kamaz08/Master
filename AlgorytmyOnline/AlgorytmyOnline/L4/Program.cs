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
                int mh, mt, rh, rt;
                mh = mt = rh = rt = 0;

                for (int t = 0; t < 100; t++)
                {
                    var test = GetRandomValues(1024, random);
                    var hiper = new HiperKostkaCost();
                    var torus = new TorusCost();
                    mh += new MoveToMinPageMigration(hiper).Calculate(test);
                    mt += new MoveToMinPageMigration(torus).Calculate(test);
                    rh += new RandomPageMigration(hiper).Calculate(test);
                    rt += new RandomPageMigration(torus).Calculate(test);
                }
                mh /= 100;
                mt /= 100;
                rh /= 100;
                rt /= 100;
                var graph = new GraphGenerator(1000, 600);
                graph.AddSeries("mtmhyper", System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column, new List<double> { 0.0 }, new List<double> { mh }, Color.Green);
                graph.AddSeries("randhyper", System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column, new List<double> { 0.0 }, new List<double> { rh }, Color.Yellow);
                graph.AddSeries("mtmtorus", System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column, new List<double> { 1.0 }, new List<double> { mt }, Color.Green);
                graph.AddSeries("randtorus", System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column, new List<double> { 1.0 }, new List<double> { rt }, Color.Yellow);
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
