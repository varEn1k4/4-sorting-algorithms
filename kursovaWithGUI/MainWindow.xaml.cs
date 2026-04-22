using System.Windows;
using System.Windows.Controls;
using Sort;
using SortAlgorithms;
using ArrayRelatedFunctions;
using Variables;

namespace kursovaWithGUI
{
    public partial class MainWindow : Window
    {

        private float[] _currentArray;
        private int _currentSize;
        private GenerationType _currentGenerationType;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnGenerateClick(object sender, RoutedEventArgs e)
        {
            if (!InputValidator.TryParseSize(textSize.Text, out int size))
            {
                MessageBox.Show("Invalid size");
                return;
            }

            if (!InputValidator.TryParseRangeMin(textMin.Text, out float minElement))
            {
                MessageBox.Show("Invalid minimum element");
                return;
            }

            if (!InputValidator.TryParseRangeMax(textMax.Text, minElement, out float maxElement))
            {
                MessageBox.Show("Invalid maximum element");
                return;
            }

            string selectedType = (AlgorithmsDirection.SelectedItem as ComboBoxItem).Content.ToString();
            GenerationType generationType = 0;

            switch (selectedType)
            {
                case "Random":
                    generationType = GenerationType.Random;
                    break;

                case "Ascending": 
                    generationType = GenerationType.Ascending;
                    break;

                case "Descending":
                    generationType = GenerationType.Descending;
                    break;
            }
            
            _currentArray = ArrayManager.CreateArray(size, minElement, maxElement, generationType);
            _currentSize = size;
            _currentGenerationType = generationType;
        }

        private void btnSortClick(object sender, RoutedEventArgs e)
        {
            if (_currentArray == null)
            {
                MessageBox.Show("Generate an array first");
                return;
            }

            float[] arrayToSort = (float[])_currentArray.Clone();
            BaseSorter sorter = null;
            string algName = (chooseAlgorithms.SelectedItem as ComboBoxItem).Content.ToString();

            switch (algName)
            {
                case "Counting Sort":
                    sorter = new CountingSort();
                    break;

                case "Radix Sort":
                    sorter = new RadixSort();
                    break;

                case "Bucket Sort":
                    sorter = new BucketSort();
                    break;

                case "Flash Sort":
                    sorter = new FlashSort();
                    break;
            }

            bool isAscending = rbAsc.IsChecked == true;

            ResultsAfterSorting results = isAscending
                ? sorter.Ascending(arrayToSort)
                : sorter.Descending(arrayToSort);

            if (sorter.SortFailed)
            {
                lblTime.Text = "Time: failed";
                lblCompares.Text = "Compare: ---";
                lblSwaps.Text = "Swaps: ---";
                MessageBox.Show("memory limit");
            }
            else
            {
                lblTime.Text = $"Time: {results.ExecutionTimeMs}";
                lblCompares.Text = $"Compare: {results.CompareAmount}";
                lblSwaps.Text = $"Swaps: {results.SwapsAmount}";
            }

            FileOperations.SaveFinalResult(sorter, results, algName, isAscending, _currentSize, _currentGenerationType);

            if (!sorter.SortFailed)
            {
                AlgorithmData data = new AlgorithmData
                {
                    Name = algName,
                    Direction = isAscending ? "Ascending" : "Descending",
                    Size = _currentSize,
                    CompareAmount = (int)results.CompareAmount,
                    SwapsAmount = (int)results.SwapsAmount,
                    Time = results.ExecutionTimeMs,
                };
                Visualization.AddAlgorithmStats(data);
            }
        }

        private void btnDrawChartClick(object sender, RoutedEventArgs e)
        {
            Visualization.SaveChartsAsImages();

            MessageBox.Show("Charts have been successfully generated and saved to the 'Images' folder",
                            "Success", MessageBoxButton.OK, MessageBoxImage.Information);

            Visualization.ClearStats();
        }

        private void btnExitClick(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}