using Sort;
using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using Variables;

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

        public static void SaveFinalResult(BaseSorter sorter, ResultsAfterSorting results, string algName, bool ascending, int size, GenerationType type)
        {
            if (sorter.SortFailed)
            {
                string direction = ascending ? "Ascending" : "Descending";
                string timeNow = DateTime.Now.ToString("HH:mm:ss");
                string errorMesage = $"[{timeNow}] {algName} ({direction}) | Size: {size} | Failed: StackOverflow";
                FileLever(errorMesage);
            }
            else
            {
                WriteResultToFile(results, algName, ascending, size, type);
            }
        }

        public static List<AlgorithmData> GetAlgorithmDatas()
        {
            List<AlgorithmData> latestResults = new List<AlgorithmData>();

            string[] lines = File.ReadAllLines(Constants.ResultFile);

            string currentDirection = null;
            int currentSize = 0;

            for (int i = lines.Length - 1; i >= 0; i--)
            {
                string line = lines[i];

                if (line == $"{Constants.ChangeArrayLever}" || line == $"{Constants.EndOfSession}")
                {
                    if (latestResults.Count > 0 && latestResults.Count < 4)
                    {
                        latestResults.Clear();
                    }

                    continue;
                }

                AlgorithmData algData = ParseLogLineData(line);

                if (algData != null)
                {
                    if (latestResults.Count == 0)
                    {
                        currentDirection = algData.Direction;
                        currentSize = algData.Size;
                        latestResults.Add(algData);
                    }
                    else if (algData.Direction == currentDirection && algData.Size == currentSize)
                    {
                        latestResults.Add(algData);
                    }
                    else
                    {
                        latestResults.Clear();
                        currentDirection = algData.Direction;
                        currentSize = algData.Size;
                        latestResults.Add(algData);
                    }
                }

                if (latestResults.Count == 4)
                {
                    break;
                }
            }

            latestResults.Reverse();
            return latestResults;
        }

        public static AlgorithmData ParseLogLineData(string line)
        {
            try
            {
                string[] parts = line.Split('|');
                if (parts.Length < 5)
                {
                    return null;
                }

                string part0 = parts[0];
                int lastBacketIndex = part0.IndexOf(']');
                string nameAndDirection = part0.Substring(lastBacketIndex + 1);

                int parenStart = nameAndDirection.IndexOf('(');
                int parenEnd = nameAndDirection.IndexOf(')');
                string name = nameAndDirection.Substring(0, parenStart).Trim();
                string direction = nameAndDirection.Substring(parenStart + 1, parenEnd - parenStart - 1).Trim();

                int size = int.Parse(parts[1].Replace("Size:", "").Trim());

                double time = double.Parse(parts[2].Replace("Time:", "").Replace("ms", "").Trim(), System.Globalization.CultureInfo.InvariantCulture);

                int compareAmount = int.Parse(parts[3].Replace("Compare:", "").Trim());

                int swapsAmount = int.Parse(parts[4].Replace("Swaps:", "").Trim());

                return new AlgorithmData
                {
                    Name = name,
                    Direction = direction,
                    Size = size,
                    Time = time,
                    CompareAmount = compareAmount,
                    SwapsAmount = swapsAmount
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}");
                return null;
            }
        }
    }
}