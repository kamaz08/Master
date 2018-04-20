using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;
using Lista2.Graph;
using Lista2.Hash;
using Lista2.Model;

namespace Lista2.List
{
    public class Lista2
    {
        public void Zad3()
        {
            var set = new GoodMultiset(1, 10000);
            var generator = new KMin();
            var crc = generator.Kmin(400, new Crc64Function(), set);
            GenerateGraphZ3("CRC64", generator, crc);
            var md5 = generator.Kmin(400, new Md5Function(), set);
            GenerateGraphZ3("MD5", generator, md5);
            var sha256 = generator.Kmin(400, new Sha256Function(), set);
            GenerateGraphZ3("SHA256", generator, sha256);
        }

        public void GenerateGraphZ3(string graphName, KMin kMin, Dictionary<int, List<double>> dictionary)
        {
            new[] { 2, 3, 10, 20, 100, 400 }.ToList().ForEach(k =>
            {
                var result = kMin.GetForK(k, dictionary);
                var graphGenerator = new GraphGenerator();
                graphGenerator.AddSeries($"{graphName}{k}", SeriesChartType.Point, result.Select(x => (double)x.Key).ToList(), result.Select(x => x.Value / x.Key).ToList());
                graphGenerator.SaveGraph($"{graphName}_k{k}");
            });
        }

        public void Zad4a()
        {
            var set = new GoodMultiset(1, 10000);
            var generator = new KMin();
            var dek = generator.Kmin(400, new Dekunction(), set);
            GenerateGraphZ3("dek", generator, dek);
        }

        public void Zad4b()
        {
            var set = new GoodMultiset(1, 10000);
            var generator = new KMin();
            var dek = generator.Kmin(400, new Crc64CutFunction(7), set);
            GenerateGraphZ3("crc64_cut_7", generator, dek);

            dek = generator.Kmin(400, new Crc64CutFunction(6), set);
            GenerateGraphZ3("crc64_cut_6", generator, dek);

        }

        public void Zad5()
        {
            var set = new GoodMultiset(1, 10000);
            var generator = new KMin();
            var crc = generator.Kmin(400, new Crc64Function(), set);
            GenerateGraphZ5($"Zad5CRC64", generator, crc, 400);

            var md5 = generator.Kmin(400, new Md5Function(), set);
            GenerateGraphZ5($"Zad5MD5", generator, md5, 400);

            var sha = generator.Kmin(400, new Sha256Function(), set);
            GenerateGraphZ5($"Zad5SHA256", generator, sha, 400);
        }

        public void GenerateGraphZ5(string graphName, KMin kMin,
            Dictionary<int, List<double>> dictionary, int k)
        {
            new[] { 0.05, 0.01, 0.005 }.ToList().ForEach(prop =>
            {
                var result = kMin.GetForK(400, dictionary);
                var graphGenerator = new GraphGenerator();
                graphGenerator.AddSeries($"{graphName}{k}", SeriesChartType.Point, result.Select(x => (double)x.Key).ToList(), result.Select(x => x.Value / x.Key).ToList());
                var czyby = Czybyszew(prop, k);
                graphGenerator.AddSeries($"{graphName}Czybyszew{k}", SeriesChartType.Line, new[] { 1.0, 10000.0 }.ToList(), new[] { 1.0 - czyby, 1.0 - czyby }.ToList(), Color.Red);
                graphGenerator.AddSeries($"{graphName}Czybyszew2{k}", SeriesChartType.Line, new[] { 1.0, 10000.0 }.ToList(), new[] { 1.0 + czyby, 1.0 + czyby }.ToList(), Color.Red);
                var chern = Chernoff(k, prop);
                graphGenerator.AddSeries($"{graphName}Chernoff{k}", SeriesChartType.Line, new[] { 1.0, 10000.0 }.ToList(), new[] { 1.0 - chern, 1.0 - chern }.ToList(), Color.Yellow);
                graphGenerator.AddSeries($"{graphName}Chernoff2{k}", SeriesChartType.Line, new[] { 1.0, 10000.0 }.ToList(), new[] { 1.0 + chern, 1.0 + chern }.ToList(), Color.Yellow);

                var mid = TruePropability(result, prop);
                graphGenerator.AddSeries($"{graphName}true{k}", SeriesChartType.Line, new[] { 1.0, 10000.0 }.ToList(), new[] { 1.0 - mid, 1.0 - mid }.ToList(), Color.Green);
                graphGenerator.AddSeries($"{graphName}true2{k}", SeriesChartType.Line, new[] { 1.0, 10000.0 }.ToList(), new[] { 1.0 + mid, 1.0 + mid }.ToList(), Color.Green);


                graphGenerator.SaveGraph($"{graphName}_prop{prop}");
            });
        }


        public double TruePropability(List<ItemDouble> list, double prop)
        {
            double l = 0.0, r = 1.0, mid = (l + r) / 2.0;
            double pr = 0;
            do
            {
                pr = list.Count(x => Math.Abs(1.0 - (x.Value / x.Key)) >= mid) / (double)list.Count();
                if (Math.Abs(pr - prop) < 0.000000001) return mid;
                if (pr < prop) r = mid;
                else l = mid;
                mid = (l + r) / 2.0;
            } while (Math.Abs(pr - prop) > 0.0000001);
            return mid;
        }

        public double Czybyszew(double prop, int k)
        {
            return Math.Sqrt(1.0 / ((k - 2) * prop));
        }

        public double Chernoff(int k, double prop)
        {
            double outa;
            double l, r, mid = 0.5;
            l = 0.0;
            r = 1.0;
            while (l + double.Epsilon <= r)
            {
                mid = (l + r) / 2;
                outa = 1 - Math.Exp(k * mid) * Math.Pow(1.0 - mid, k) - Math.Exp(k * -mid) * Math.Pow(1.0 + mid, k);
                if (Math.Abs(outa - (1.0 - prop)) < 0.0000000001) break;
                if (outa < (1.0 - prop))
                    l = mid;
                else
                    r = mid;
            }
            return mid;
        }


        public void test()
        {
            var set = new StupidMultiset(1, 10000);
            var generator = new KMin();
            var crc = generator.Kmin(400, new Crc64Function(), set);
            GenerateGraphZ3("CRC64stupid", generator, crc);
        }
    }
}
