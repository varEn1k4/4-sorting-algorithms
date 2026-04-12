using ArrayRelatedFunctions;
using Sort;
using System;
using System.Collections.Generic;
using System.Text;

namespace SortAlgorithms
{
    public class CountingSort : BaseSorter
    {
        protected override void SortAscending(float[] array)
        {
            long[] arrayLong = new long[array.Length];
            for (int i = 0; i < array.Length; i++)
            {
                arrayLong[i] = (long)Math.Round(array[i] * 1e5F) ;
            }

            long maxElement = 0;

            for (int i = 0; i < arrayLong.Length; i++)
            {
                comparisons++;
                if (arrayLong[i] > maxElement)
                {
                    maxElement = arrayLong[i];
                }
            }

            long[] arrayLongForIndexes = new long[(int)maxElement + 1];

            for (int i = 0; i < arrayLong.Length; i++)
            {
                arrayLongForIndexes[arrayLong[i]]++;
                swaps++;
            }

            for (int i = 1; i < arrayLongForIndexes.Length; i++)
            {
                arrayLongForIndexes[i] = arrayLongForIndexes[i - 1] + arrayLongForIndexes[i];
                swaps++;
            }

            long[] resultArray = new long[arrayLong.Length];

            for (int i = arrayLong.Length - 1; i >= 0; i--)
            {
                resultArray[arrayLongForIndexes[arrayLong[i]] - 1] = arrayLong[i];
                arrayLongForIndexes[arrayLong[i]]--;
                swaps++;
            }

            for (int i = 0; i < array.Length; i++)
            {
                array[i] = (float)Math.Round(resultArray[i] / 1e5D);
            }

        }

        protected override void SortDescending(float[] array)
        {
            long[] arrayLong = new long[array.Length];
            for (int i = 0; i < array.Length; i++)
            {
                arrayLong[i] = (long)Math.Round(array[i] * 1e5F);
            }

            long maxElement = 0;

            for (int i = 0; i < arrayLong.Length; i++)
            {
                comparisons++;
                if (arrayLong[i] > maxElement)
                {
                    maxElement = arrayLong[i];
                }
            }

            long[] arrayLongForIndexes = new long[(int)maxElement + 1];

            for (int i = 0; i < arrayLong.Length; i++)
            {
                arrayLongForIndexes[arrayLong[i]]++;
                swaps++;
            }

            for (int i = arrayLongForIndexes.Length - 2; i >= 0; i--)
            {
                arrayLongForIndexes[i] = arrayLongForIndexes[i] + arrayLongForIndexes[i + 1];
                swaps++;
            }

            long[] resultArray = new long[arrayLong.Length];

            for (int i = arrayLong.Length - 1; i >= 0; i--)
            {
                resultArray[arrayLongForIndexes[arrayLong[i]] - 1] = arrayLong[i];
                arrayLongForIndexes[arrayLong[i]]--;
                swaps++;
            }

            for (int i = 0; i < array.Length; i++)
            {
                array[i] = (float)Math.Round(resultArray[i] / 1e5D);
            }
        }
    }
}
