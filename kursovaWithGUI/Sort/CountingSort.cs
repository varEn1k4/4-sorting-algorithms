using Sort;

namespace SortAlgorithms
{
    public class CountingSort : BaseSorter
    {
        protected override void SortAscending(float[] array)
        {
            CountingSortMain(array, true);
        }

        protected override void SortDescending(float[] array)
        {
            CountingSortMain(array, false);
        }

        private void CountingSortMain(float[] array, bool ascending)
        {
            if (array.Length == 0)
            {
                return;
            }

            int multiplier = InputValidator.GetDynamicMultiplier(array);

            long[] arrayLong = new long[array.Length];

            for (int i = 0; i < array.Length; i++)
            {
                arrayLong[i] = (long)Math.Round(array[i] * multiplier);
            }

            long minElement = arrayLong[0];
            long maxElement = arrayLong[0];

            for (int i = 1; i < arrayLong.Length; i++)
            {
                comparisons += 2;

                if (arrayLong[i] < minElement)
                {
                    minElement = arrayLong[i];
                }

                if (arrayLong[i] > maxElement)
                {
                    maxElement = arrayLong[i];
                }
            }

            if (minElement < 0)
            {
                for (int i = 0;  i < arrayLong.Length; i++)
                {
                    arrayLong[i] -= minElement;
                }
                maxElement -= minElement;
            }

            if (maxElement < 0 || maxElement > 5e8)
            {
                Console.WriteLine("Stack Overflow");
                this.SortFailed = true;
                return;
            }

            long[] arrayLongForIndexes = new long[(int)maxElement + 1];

            for (int i = 0; i < arrayLong.Length; i++)
            {
                arrayLongForIndexes[arrayLong[i]]++;
            }


            if (ascending)
            {
                for (int i = 1; i < arrayLongForIndexes.Length; i++)
                {
                    arrayLongForIndexes[i] = arrayLongForIndexes[i - 1] + arrayLongForIndexes[i];
                }
            }
            else
            {
                for (int i = arrayLongForIndexes.Length - 2; i >= 0; i--)
                {
                    arrayLongForIndexes[i] = arrayLongForIndexes[i] + arrayLongForIndexes[i + 1];
                }
            }

            long[] resultArray = new long[arrayLong.Length];

            for (int i = arrayLong.Length - 1; i >= 0; i--)
            {
                resultArray[arrayLongForIndexes[arrayLong[i]] - 1] = arrayLong[i];
                arrayLongForIndexes[arrayLong[i]]--;
                swaps++;
            }

            if (minElement < 0)
            {
                for (int i = 0;  i < resultArray.Length; i++)
                {
                    resultArray[i] += minElement;
                }
            }

            for (int i = 0; i < array.Length; i++)
            {
                array[i] = (float)Math.Round(resultArray[i] / (double)multiplier, 5);
            }
        }
    }
}
