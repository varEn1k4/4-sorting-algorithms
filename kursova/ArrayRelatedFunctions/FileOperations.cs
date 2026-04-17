using System;
using System.Collections.Generic;
using System.Text;

namespace ArrayRelatedFunctions
{
    public class FileOperations
    {
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
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
            }

        }
        public static void FileLever(string message)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(Constants.ResultFile, true))
                {
                    sw.WriteLine(message);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
            }
        }
    }
}
