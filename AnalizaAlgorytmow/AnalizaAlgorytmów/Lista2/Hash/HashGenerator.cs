using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms.DataVisualization.Charting;

namespace Lista2.Hash
{
    public class HashGenerator
    {
        public static void Main(string[] args)
        {
            Console.Out.WriteLine("heheh");

            var test = new HashGenerator();



            test.Kmin(400, new Sha256Function());
            //test.Kmin(400, new Crc64Function());
            //test.Kmin(400,new Md5Function());

        }

        public void GenerateGraph(List<ItemDouble> list, int k)
        {
            var prosze = list.Select(x => Math.Abs(1 - x.Value / x.Key)).Where(x => x > 3);


            Chart chart = new Chart();

            chart.Size = new Size(10000, 500);

            var chartArea = new ChartArea();
            chartArea.AxisX.MajorGrid.LineColor = Color.LightGray;
            chartArea.AxisY.MajorGrid.LineColor = Color.LightGray;
            chartArea.AxisX.LabelStyle.Font = new Font("Consolas", 8);
            chartArea.AxisY.LabelStyle.Font = new Font("Consolas", 8);
            chart.ChartAreas.Add(chartArea);

            var series = new Series();
            series.Name = "Series1";
            series.ChartType = SeriesChartType.Point;
            series.XValueType = ChartValueType.Double;
            chart.Series.Add(series);



            // bind the datapoints
            chart.Series["Series1"].Points.DataBindXY(list.Select(x => x.Key).ToArray(), list.Select(x => Math.Abs(1 - x.Value / x.Key)).ToArray());

            //var series005 = new Series();
            //series005.Name = "Series005";
            //series005.ChartType = SeriesChartType.Area;
            //series005.XValueType = ChartValueType.Double;
            //series005.YValueType = ChartValueType.Double;
            //series005.Color = Color.FromArgb(50, Color.Red);
            //chart.Series.Add(series005);
            //chart.Series["Series005"].Points.DataBindXY(new[] {1, 10000}, new[] { 0.0914, 0.0914 });

            // draw!
            chart.Invalidate();

            // write out a file
            chart.SaveImage($"chartbadCRC64_7_{k}.png", ChartImageFormat.Png);

        }


        public void Kmin(int k, IHashFunction func)
        {
            var set = new GoodMultiset(1, 10000);

            var result = new Dictionary<int, List<double>>();

            for (int i = 10000; i > 0; i--)
            {
                var s = set.GetMultiset(i - 1);
                var ires = new List<double>();

                s.ForEach(x =>
                {
                    var prop = func.GetHash(x);
                    ires = AddElement(ires, k, prop);
                });
                result.Add(i, ires);
            }

            var k2 = GetForK(2, result);
            var k3 = GetForK(3, result);
            var k10 = GetForK(10, result);
            var k100 = GetForK(100, result);
            var k400 = GetForK(400, result);

            GenerateGraph(k2, 2);
            GenerateGraph(k3, 3);
            GenerateGraph(k10, 10);
            GenerateGraph(k100, 100);
            GenerateGraph(k400, 400);

            var hehe = new List<double>();

            for (int i = 2; i <= 400; i++)
            {
                var temp = GetForK(i, result);
                hehe.Add(temp.Count(x=> Math.Abs(1-(x.Value/x.Key))>0.1) / (double) temp.Count());
            }

            var a = k400.Count(x => Math.Abs(1 - (x.Value / x.Key)) > 0.1357) / (double) 10000.0;

        }

        private List<double> AddElement(List<double> list, int maks, double el)
        {
            int i = list.Count() - 1;
            for (; i >= 0; i--)
            {
                if (list[i] == el) return list;
                if (list[i] < el)
                {
                    if (i + 1 == maks) return list; 
                    var res = list.Take(i + 1).ToList();
                    res.Add(el);
                    res.AddRange(list.Skip(i + 1).Take(maks - i - 2));
                    return res;
                }
            }

            if (i == -1)
            {
                var res = new List<double>() { el };
                res.AddRange(list.Take(maks - 1));
                return res;
            }

            if (list.Count() < maks)
                list.Add(el);
            return list;
        }

        private List<ItemDouble> GetForK(int k, Dictionary<int, List<double>> dict)
        {
            var result = new List<ItemDouble>();
            for (int i = 1; i <= 10000; i++)
            {
                var list = dict[i];
                result.Add(new ItemDouble
                {
                    Key = i,
                    Value = list.Count() < k ? list.Count() : (k-1)/list[k-1]
                });
            }
            return result;
        }
    }
}
