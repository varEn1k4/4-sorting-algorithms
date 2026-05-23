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

        private BaseSorter GetSorterAlgorithm(AlgorithmType type)
        {
            return type switch
            {
                AlgorithmType.CountingSort => new CountingSort(),
                AlgorithmType.RadixSort => new RadixSort(),
                AlgorithmType.BucketSort => new BucketSort(),
                AlgorithmType.FlashSort => new FlashSort(),
            };
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

            GenerationType generationType = (GenerationType)(AlgorithmsDirection.SelectedIndex + 1);
            
            _currentArray = ArrayManager.CreateArray(size, minElement, maxElement, generationType);
            _currentSize = size;
            _currentGenerationType = generationType;
        }

        private async void btnSortClick(object sender, RoutedEventArgs e)
        {
            if (_currentArray == null)
            {
                MessageBox.Show("Generate an array first");
                return;
            }


            btnSort.IsEnabled = false;
            btnGenerate.IsEnabled = false;
            chooseAlgorithms.IsEnabled = false;

            try
            {
                float[] arrayToSort = (float[])_currentArray.Clone();

                AlgorithmType algType = (AlgorithmType)(chooseAlgorithms.SelectedIndex + 1);
                string algName = (chooseAlgorithms.SelectedItem as ComboBoxItem).Content.ToString();
                SortDirection direction = rbAsc.IsChecked == true
                    ? SortDirection.Ascending
                    : SortDirection.Descending;

                BaseSorter sorter = GetSorterAlgorithm(algType);

                if (cbAnimate.IsChecked == true)
                {
                    sorter.OnStep = Visualization.CreateAnimationAction(SortCanvas);
                }
                else
                {
                    sorter.OnStep = null;
                }

                ResultsAfterSorting results = null;

                await Task.Run(() =>
                {
                    results = (direction == SortDirection.Ascending)
                    ? sorter.Ascending(arrayToSort)
                    : sorter.Descending(arrayToSort);
                });

                lblTime.Text = $"Time: {results.ExecutionTimeMs}";
                lblCompares.Text = $"Compare: {results.CompareAmount}";
                lblSwaps.Text = $"Swaps: {results.SwapsAmount}";

                FileOperations.SaveFinalResult(results, algName, direction, _currentSize, _currentGenerationType);
            }
            finally
            {
                btnSort.IsEnabled = true;
                btnGenerate.IsEnabled = true;
                chooseAlgorithms.IsEnabled = true;
            }
           
        }
        private void btnExitClick(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}