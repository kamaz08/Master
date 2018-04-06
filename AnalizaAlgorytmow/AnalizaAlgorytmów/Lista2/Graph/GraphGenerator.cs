using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;

namespace Lista2.Graph
{
    public class GraphGenerator
    {
        private Chart _chart;


        public GraphGenerator() : this(10000, 500)
        {
        }

        public GraphGenerator(int width, int height)
        {
            _chart = new Chart();
            _chart.Size = new Size(width, height);
            var chartArea = new ChartArea();
            chartArea.AxisX.MajorGrid.LineColor = Color.LightGray;
            chartArea.AxisY.MajorGrid.LineColor = Color.LightGray;
            chartArea.AxisX.LabelStyle.Font = new Font("Consolas", 8);
            chartArea.AxisY.LabelStyle.Font = new Font("Consolas", 8);
            _chart.ChartAreas.Add(chartArea);
        }

        public void AddSeries(string seriesName, SeriesChartType type, List<double> xValueList, List<double> yValueList, Color? color = null)
        {
            var series = new Series();
            series.Name = seriesName;
            series.ChartType = type;
            series.XValueType = ChartValueType.Double;
            if(color.HasValue) series.Color = color.Value;
            _chart.Series.Add(series);
            _chart.Series[seriesName].Points.DataBindXY(xValueList, yValueList);
        }

        public void SaveGraph(string fileName)
        {
            _chart.Invalidate();
            _chart.SaveImage($"{fileName}.png", ChartImageFormat.Png);
        }
    }
}
