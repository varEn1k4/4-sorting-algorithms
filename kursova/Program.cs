using ArrayRelatedFunctions;
using Sort;
using SortAlgorithms;

public class Program
{
    public static void Main(string[] args)
    {
        string repeatProgramAgain;
        do
        {
            int size = ConsoleUI.GetSize();
            float min = ConsoleUI.GetMin();
            float max = ConsoleUI.GetMax(min);
            float[] array = ArrayRelatedFunctions.ArrayManager.CreateArray(size, min, max, ConsoleUI.GetGenerationType());

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
            ConsoleUI.

            Console.WriteLine("Do you want to star programm again (1 - yes; 0 - no): ");
            repeatProgramAgain = Console.ReadLine();
        } while (repeatProgramAgain == "1");

    }
}