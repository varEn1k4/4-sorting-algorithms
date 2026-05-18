using Sort;
using System.IO;
using Variables;

namespace ArrayRelatedFunctions
{
    public class FileOperations
    {
        public static void SaveFinalResult(ResultsAfterSorting results, string algName, SortDirection direction, int size, GenerationType type)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(Constants.ResultFile, true))
                {
                    string time = DateTime.Now.ToString("HH:mm:ss");
                    sw.WriteLine($"[{time}] {algName} ({direction}) | Size: {size} | Time: {results.ExecutionTimeMs}ms | Compare: {results.CompareAmount} | Swaps: {results.SwapsAmount}");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
            }
        }
    }
}