using System.IO;
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

            var lastRun = _stats.Last();

            var filteredStats = _stats
                .Where(s => s.Size == lastRun.Size && s.Direction == lastRun.Direction)
                .GroupBy(s => s.Name)
                .Select(group => group.Last())
                .ToList();

            if (!Directory.Exists(Constants.ImagesFolder))
            {
                Directory.CreateDirectory(Constants.ImagesFolder);
            }

            SaveSingleImage(Path.Combine(Constants.ImagesFolder, "Time_chart.png"), "Working time (ms)", s => s.Time, filteredStats);
            SaveSingleImage(Path.Combine(Constants.ImagesFolder, "Compare_chart.png"), "Compare amount", s => s.CompareAmount, filteredStats);
            SaveSingleImage(Path.Combine(Constants.ImagesFolder, "Swaps_chart.png"), "Swaps amount", s => s.SwapsAmount, filteredStats);
        }

        private static void SaveSingleImage(string fileName, string Ylabel, Func<AlgorithmData, double> valueSelector, List<AlgorithmData> dataToProcess)
        {
            Plot myPlot = new Plot();

            double[] values = dataToProcess.Select(valueSelector).ToArray();
            string[] labels = dataToProcess.Select(s => s.Name).ToArray();

            var bars = myPlot.Add.Bars(values);

            ScottPlot.TickGenerators.NumericManual tick = new ScottPlot.TickGenerators.NumericManual();

            for (int i = 0; i < labels.Length; i++)
            {
                tick.AddMajor(i, labels[i]);
            }
            myPlot.Axes.Bottom.TickGenerator = tick;

            myPlot.YLabel(Ylabel);
            myPlot.XLabel("algorithm");

            myPlot.Title($"Size: {dataToProcess[0].Size} | {dataToProcess[0].Direction}");

            myPlot.SavePng(fileName, 800, 600);
        }
    }

}