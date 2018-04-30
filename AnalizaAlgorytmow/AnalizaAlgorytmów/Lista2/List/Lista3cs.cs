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
    public class Lista3cs
    {
        private MultisetAbstract set;
        private Dictionary<int, Dictionary<decimal, int>> _dictHyper;
        public Lista3cs()
        {
            //int b = 6;
            set = new GoodMultiset(1, 10000);
            //var hash = new Sha256ByteFunction();
            //_dictHyper = HyperLogLog.Execute(b, hash, set);
        }
        public void TestSHA()
        {
            var list = new[] {4, 5, 6}.ToList();
            list.ForEach(b =>
            {
                var res = HyperLogLog.GetResult(_dictHyper, b);
                var graphGenerator = new GraphGenerator();
                graphGenerator.AddSeries($"SHA256{b}", SeriesChartType.Point, res.Select(x => (double)x.Key).ToList(), res.Select(x => x.Value / x.Key).ToList());
                graphGenerator.SaveGraph($"SHA256_b_{b}");
            });
          
        }



        public void TestMD5()
        {
            var hash = new Md5FunctionByte();
            var dictHyper = HyperLogLog.Execute(6, hash, set);

            var list = new[] {4, 5, 6}.ToList();
            list.ForEach(b =>
            {
                var res = HyperLogLog.GetResult(dictHyper, b);
                var graphGenerator = new GraphGenerator();
                graphGenerator.AddSeries($"md5 {b}", SeriesChartType.Point, res.Select(x => (double) x.Key).ToList(),
                    res.Select(x => x.Value / x.Key).ToList());
                graphGenerator.SaveGraph($"MD5_{b}");

            });
        }


        public void TestDek()
        {
            var hash = new DekFunctionByte();
            var dictHyper = HyperLogLog.Execute(4, hash, set);

            var list = new[] { 4,5,6 }.ToList();
            list.ForEach(b =>
            {
                var res = HyperLogLog.GetResult(dictHyper, b);
                var graphGenerator = new GraphGenerator();
                graphGenerator.AddSeries($"md5 {b}", SeriesChartType.Point, res.Select(x => (double)x.Key).ToList(),
                    res.Select(x => x.Value / x.Key).ToList());
                graphGenerator.SaveGraph($"DEK_{b}");

            });
        }


        public void Campare()
        {
            int k = 5;
            var generator = new KMin();
            var kMin = generator.Kmin(5, new Sha256Function(), set);
            var result = generator.GetForK(5, kMin);

            var graphGenerator = new GraphGenerator();
            graphGenerator.AddSeries($"SHA256_kmin5", SeriesChartType.Point, result.Select(x => (double)x.Key).ToList(), result.Select(x => x.Value / x.Key).ToList(),Color.Red);
            var res = HyperLogLog.GetResult(_dictHyper, k);
            graphGenerator.AddSeries($"SHA256_hyper", SeriesChartType.Point, res.Select(x => (double)x.Key).ToList(), res.Select(x => x.Value / x.Key).ToList(),Color.Blue);
            graphGenerator.SaveGraph($"COMPARE_k{k}");
        }
    }
}
