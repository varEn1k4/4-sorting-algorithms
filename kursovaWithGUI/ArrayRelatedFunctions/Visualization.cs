using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using ScottPlot;
using Variables;

namespace ArrayRelatedFunctions
{
    public class AlgorithmData
    {
        public string Name { get; set; }
        public string Direction { get; set; }
        public int Size { get; set; }
        public int CompareAmount { get; set; }
        public int SwapsAmount { get; set; }
        public double Time { get; set; }
    }
    public static class Visualization
    {
        private static List<AlgorithmData> _stats = new List<AlgorithmData>();

        public static void AddAlgorithmStats(AlgorithmData data)
        {
            _stats.Add(data);
        }

        public static void ClearStats()
        {
            _stats.Clear();
        }

        public static void SaveChartsAsImages()
        {
            if (_stats.Count == 0)
            {
                return;
            }

            if (!Directory.Exists(Constants.ImagesFolder))
            {
                Directory.CreateDirectory(Constants.ImagesFolder);
            }

            SaveSingleImage(Path.Combine(Constants.ImagesFolder, "Time_chart.png"), "Working time (ms)", s => s.Time);
            SaveSingleImage(Path.Combine(Constants.ImagesFolder, "Compare_chart.png"), "Compare amount", s => s.CompareAmount);
            SaveSingleImage(Path.Combine(Constants.ImagesFolder, "Swaps_chart.png"), "Swaps amount", s => s.SwapsAmount);
        }

        private static void SaveSingleImage(string fileName, string Ylabel, Func<AlgorithmData, double> valueSelector)
        {
            Plot myPlot = new Plot();

            double[] values = _stats.Select(valueSelector).ToArray();
            string[] labels = _stats.Select(s => s.Name).ToArray();

            var bars = myPlot.Add.Bars(values);

            ScottPlot.TickGenerators.NumericManual tick = new ScottPlot.TickGenerators.NumericManual();

            for (int i = 0; i < labels.Length; i++)
            {
                tick.AddMajor(i, labels[i]);
            }
            myPlot.Axes.Bottom.TickGenerator = tick;

            myPlot.YLabel(Ylabel);
            myPlot.XLabel("algorithm");

            myPlot.Title($"Size: {_stats[0].Size} | {_stats[0].Direction}");

            myPlot.SavePng(fileName, 800, 600);
        }
    }

}