using System;
using System.Collections.Generic;
using System.Text;

namespace ArrayRelatedFunctions
{
    public static class ArrayManager
    {
        public static float[] CreateArray(int size, float min, float max, GenerationType type)
        {
            Random random = new Random();
            float[] array = new float[size];

            for (int i = 0; i < size; i++)
            {
                array[i] = (float)(random.NextDouble() * (max - min) + min); 
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

        public static void WriteResultToFile(ResultsAfterSorting results, string algName, bool ascending, int size, GenerationType type)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(Constants.ResultFile, true))
                {
                    string direction = ascending ? "Ascending" : "Descending";
                    string time = DateTime.Now.ToString("HH:mm:ss");

                    sw.WriteLine($"[{time}] {algName} ({direction}) | Size: {size} | Time: {results.ExecutionTimeMs}ms | Compare: {results.CompareAmount} | Swaps: {results.SwapsAmount}");
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
