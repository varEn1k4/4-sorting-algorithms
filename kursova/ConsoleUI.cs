using ArrayRelatedFunctions;
using Sort;
using SortAlgorithms;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

public class ConsoleUI
{
    public static int GetSize()
    {
        bool isSizeOfArrayCorrect = false;
        int sizeOfArray = 0;
        do
        {
            Console.Write("Enter size of array (from 100 elements to 50000): ");
            string input = Console.ReadLine();

            isSizeOfArrayCorrect = int.TryParse(input, out sizeOfArray) && sizeOfArray >= 100 && sizeOfArray <= 50000;

            if (!isSizeOfArrayCorrect)
            {
                Console.WriteLine("wrong input");
            }
        } while (!isSizeOfArrayCorrect);

        return sizeOfArray;
    }

    public static float GetMin()
    {
        bool isMinCorrect = false;
        float min = 0;
        do
        {
            Console.Write("Enter minimal possible element (min = -1e7; max = 1e7; precision = 1e-5): ");
            string input = Console.ReadLine();

            isMinCorrect = float.TryParse(input, out min) && min >= -1e7F && min <= 1e7F;

            if (isMinCorrect && min != 0 && Math.Abs(min) < 1e-5f)
            {
                Console.WriteLine("Warning: Input value is too small");
                isMinCorrect = false;
            }

            if (!isMinCorrect)
            {
                Console.WriteLine("Error: Wrong input");
            }

        } while (!isMinCorrect);

        return (float)Math.Round(min, 5);
    }

    public static float GetMax(float min)
    {
        bool isMaxCorrect = false;
        float max = 0;
        do
        {
            Console.Write($"Enter maximal possible element (min = {min + 1D}; max = 1e7): ");
            string input = Console.ReadLine();

            isMaxCorrect = float.TryParse(input, out max) && max >= min + 1F && max <= 1e7F;

            if (isMaxCorrect && min != 0 && Math.Abs(max) < 1e-5f)
            {
                Console.WriteLine("Warning: Input value is too small");
                isMaxCorrect = false;
            }

            if (!isMaxCorrect)
            {
                Console.WriteLine("wrong input 2");
            }
        } while (!isMaxCorrect);
        return max;
    }

    public static GenerationType GetGenerationType()
    {
        ushort choice = 0;
        bool isChoiceCorrect = false;
        do
        {
            Console.WriteLine("choose type of array:");
            Console.WriteLine("1 - random");
            Console.WriteLine("2 - sorted");
            Console.WriteLine("3 - reversed sorted");

            string input = Console.ReadLine();

            isChoiceCorrect = ushort.TryParse(input, out choice) && choice >= 1 && choice <= 3;

            if (!isChoiceCorrect)
            {
                Console.WriteLine("wrong input");
            }
        } while (!isChoiceCorrect);

        return (GenerationType)choice;
    }

    public static AlgorithmType GetAlgorithmType()
    {
        ushort choice = 0;
        bool isAlgTypeCorrect= false;
        do
        {
            Console.WriteLine("choose algorithm:");
            Console.WriteLine("1 - CountingSort");
            Console.WriteLine("2 - RadixSort");
            Console.WriteLine("3 - BucketSort");
            Console.WriteLine("4 - FlashSort");

            string input = Console.ReadLine();
            isAlgTypeCorrect = ushort.TryParse(input, out choice) && choice >= 1 && choice <= 4;

            if (!isAlgTypeCorrect)
            {
                Console.WriteLine("wrong input");
            }
        } while (!isAlgTypeCorrect);

        return (AlgorithmType)choice;
    }

    public static SortDirection GetSortDirection()
    {
        ushort choice = 0;
        bool isSortDirectionCorrect = false;
        do
        {
            Console.WriteLine("choose type of sorting:");
            Console.WriteLine("1 - Ascending");
            Console.WriteLine("2 - Descending");

            string input = Console.ReadLine();
            isSortDirectionCorrect = ushort.TryParse(input, out choice) && choice >= 1 && choice <= 2;

            if (!isSortDirectionCorrect)
            {
                Console.WriteLine("wrong input");
            }
        } while (!isSortDirectionCorrect);

        return (SortDirection)choice;
    }

    public static void DisplayResults(BaseSorter sorter, float[] array, string algName, bool ascending)
    {
        string direction = ascending ? "Ascending" : "Descending";
        Console.WriteLine($"Algorithm - {algName}; Direction - {direction}");

        float[] copyOfArray = (float[])array.Clone();
        Console.WriteLine($"Size of array: {copyOfArray.Length}\n");

        ResultsAfterSorting results;

        if (ascending)
        {
            results = sorter.Ascending(copyOfArray);
        }
        else
        {
            results = sorter.Descending(copyOfArray);
        }

        Console.WriteLine($"Spent Time: {results.ExecutionTimeMs}");
        Console.WriteLine($"Compare Amount: {results.CompareAmount}");
        Console.WriteLine($"Swaps Amount: {results.SwapsAmount}");
    }
}
