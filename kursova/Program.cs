using ArrayRelatedFunctions;
using Sort;
using SortAlgorithms;

public class Program
{
    public static void Main(string[] args)
    {
        bool repeatProgramAgain;
        do
        {
            bool keepSameArray;

            int size = ConsoleUI.GetSize();
            float min = ConsoleUI.GetMin();
            float max = ConsoleUI.GetMax(min);
            GenerationType generationType = ConsoleUI.GetGenerationType();
            float[] array = ArrayRelatedFunctions.ArrayManager.CreateArray(size, min, max, generationType);

            do
            {
                AlgorithmType algorithmType = ConsoleUI.GetAlgorithmType();
                SortDirection sortDirection = ConsoleUI.GetSortDirection();

                BaseSorter sorter = null;
                string algName = "";

                switch (algorithmType)
                {
                    case AlgorithmType.CountingSort:
                        sorter = new CountingSort();
                        algName = "Counting Sort";
                        break;

                    case AlgorithmType.RadixSort:
                        sorter = new RadixSort();
                        algName = "Radix Sort";
                        break;

                    case AlgorithmType.BucketSort:
                        sorter = new BucketSort();
                        algName = "Bucket Sort";
                        break;

                    case AlgorithmType.FlashSort:
                        sorter = new FlashSort();
                        algName = "Flash Sort";
                        break;
                }

                bool isAscending = sortDirection == SortDirection.Ascending;

                ResultsAfterSorting finalResults = ConsoleUI.DisplayResults(sorter, array, algName, isAscending);
                ArrayRelatedFunctions.FileOperations.WriteResultToFile(finalResults, algName, isAscending, size, generationType);
                keepSameArray = ConsoleUI.AskUserToKeepSortingWithSameArray();

            } while (keepSameArray);

            repeatProgramAgain = ConsoleUI.AskUserToRunProgramAgain();

        } while (repeatProgramAgain);
    }
}