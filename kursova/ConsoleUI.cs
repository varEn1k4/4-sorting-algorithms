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
            Console.Write($"Enter size of array (from {Constants.MinArraySize} elements to {Constants.MaxArraySize}): ");
            string input = Console.ReadLine();

            isSizeOfArrayCorrect = int.TryParse(input, out sizeOfArray)
                && sizeOfArray >= Constants.MinArraySize
                && sizeOfArray <= Constants.MaxArraySize;

            if (!isSizeOfArrayCorrect)
            {
                Console.WriteLine("Eror: wrong input");
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
            Console.Write($"Enter minimal possible element (min = {Constants.MinLimit}; max = {Constants.MaxLimit}; precision = {Constants.Precision}): ");
            string input = Console.ReadLine();

            isMinCorrect = float.TryParse(input, out min)
                && min >= Constants.MinLimit
                && min <= Constants.MaxLimit;

            if (isMinCorrect && min != 0 && Math.Abs(min) < Constants.Precision)
            {
                Console.WriteLine("Warning: Input value is too small");
                isMinCorrect = false;
            }

            if (!isMinCorrect)
            {
                Console.WriteLine("Error: Wrong input");
            }

        } while (!isMinCorrect);

        return (float)Math.Round(min, Constants.PrecisionDecimalPlaces);
    }

    public static float GetMax(float min)
    {
        bool isMaxCorrect = false;
        float max = 0;
        do
        {
            Console.Write($"Enter maximal possible element (min = {min + 1D}; max = {Constants.MaxLimit}): ");
            string input = Console.ReadLine();

            isMaxCorrect = float.TryParse(input, out max) && max >= min + 1F && max <= Constants.MaxLimit;

            if (isMaxCorrect && max != 0 && Math.Abs(max) < Constants.Precision)
            {
                Console.WriteLine("Warning: Input value is too small");
                isMaxCorrect = false;
            }

            if (!isMaxCorrect)
            {
                Console.WriteLine("Error: wrong input");
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
            Console.Write("-> ");

            string input = Console.ReadLine();

            isChoiceCorrect = ushort.TryParse(input, out choice) && choice >= 1 && choice <= 3;

            if (!isChoiceCorrect)
            {
                Console.WriteLine("Error: wrong input");
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
            Console.Write("-> ");

            string input = Console.ReadLine();
            isAlgTypeCorrect = ushort.TryParse(input, out choice) && choice >= 1 && choice <= 4;

            if (!isAlgTypeCorrect)
            {
                Console.WriteLine("Error: wrong input");
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
            Console.WriteLine("Choose type of sorting:");
            Console.WriteLine("1 - Ascending");
            Console.WriteLine("2 - Descending");
            Console.Write("-> ");

            string input = Console.ReadLine();
            isSortDirectionCorrect = ushort.TryParse(input, out choice) && choice >= 1 && choice <= 2;

            if (!isSortDirectionCorrect)
            {
                Console.WriteLine("Erroe: wrong input");
            }
        } while (!isSortDirectionCorrect);

        return (SortDirection)choice;
    }

    public static bool AskUserToKeepSortingWithSameArray()
    {
        Console.WriteLine();
        Console.Write("Do you want to keep the same array but change algorithm/direction? (1 - yes; 0 - no): ");
        string input = Console.ReadLine();
        return input == "1";
    }

    public static bool AskUserToRunProgramAgain()
    {
        Console.WriteLine();
        Console.Write("Do you want to star programm again (1 - yes; 0 - no): ");
        string input = Console.ReadLine();
        
        if (input == "1")
        {
            ArrayRelatedFunctions.FileOperations.FileLever($"{Constants.ChangeArrayLever}");
        }
        else
        {
            ArrayRelatedFunctions.FileOperations.FileLever($"{Constants.EndOfSession}");
        }

        return input == "1";
    }

    public static ResultsAfterSorting DisplayResults(BaseSorter sorter, float[] array, string algName, bool ascending)
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

        return results;
    }
}
