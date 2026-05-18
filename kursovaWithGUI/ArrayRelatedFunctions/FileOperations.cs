using Sort;
using System.IO;
using Variables;

namespace ArrayRelatedFunctions
{
    public class FileOperations
    {
        private static void WriteResultToFile(ResultsAfterSorting results, string algName, bool ascending, int size, GenerationType type)
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
        private static void FileLever(ResultsAfterSorting results, string algName, bool ascending, int size, GenerationType type)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(Constants.ResultFile, true))
                {
                    string direction = ascending ? "Ascending" : "Descending";
                    string timeNow = DateTime.Now.ToString("HH:mm:ss");
                    string errorMesage = $"[{timeNow}] {algName} ({direction}) | Size: {size} | Failed: StackOverflow";
                    sw.WriteLine(errorMesage);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
            }
        }

        public static void SaveFinalResult(BaseSorter sorter, ResultsAfterSorting results, string algName, bool ascending, int size, GenerationType type)
        {
            if (sorter.SortFailed)
            {
                FileLever(results, algName, ascending, size, type);
            }
            else
            {
                WriteResultToFile(results, algName, ascending, size, type);
            }
        }
    }
}