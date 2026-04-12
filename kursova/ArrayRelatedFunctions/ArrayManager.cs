using System;
using System.Collections.Generic;
using System.Text;

namespace ArrayRelatedFunctions
{
    public static class ArrayManager
    {
        public static float[] CreateArray(int size, float min, float max, GenerationType type)
        {
            Random rnd = new Random();
            float[] array = new float[size];

            for (int i = 0; i < size; i++)
            {
                array[i] = (float)(rnd.NextDouble() * (max - min) + min); 
            }

            switch (type)
            {
                case ArrayRelatedFunctions.GenerationType.Random:
                    break;

                case ArrayRelatedFunctions.GenerationType.Ascending:
                    Array.Sort(array);
                    break;

                case ArrayRelatedFunctions.GenerationType.Descending:
                    Array.Sort(array);
                    Array.Reverse(array);
                    break;
            }

            return array;
        }

        public static void WriteResultToFile(ResultsAfterSorting results, string algName)
        {
            string file = "results.txt";

            try
            {
                using (StreamWriter sw = new StreamWriter(file))
                {
                    sw.WriteLine($"Algorithm: {algName}, Compare: {results.CompareAmount}, Swaps: {results.SwapsAmount}, Time: {results.ExecutionTimeMs}");
                }

                Console.WriteLine("Saved");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
            }

        }
    }
}
