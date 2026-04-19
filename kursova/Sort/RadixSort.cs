using Sort;
using System;
using System.Collections.Generic;
using System.Text;
using Variables;

namespace SortAlgorithms
{
    public class RadixSort : BaseSorter
    {
        protected override void SortAscending(float[] array)
        {
            RadixSortMain(array, true);
        }

        protected override void SortDescending(float[] array)
        {
           RadixSortMain(array, false);
        }

        private void RadixSortMain(float[] array, bool ascending)
        {
            if (array.Length == 0)
            {
                return;
            }

            long[] arrayLong = new long[array.Length];
            for (int i = 0; i < array.Length; i++)
            {
                arrayLong[i] = (long)Math.Ceiling(array[i] * Constants.ConvertNumbersWithFloatingPoints);
            }

            //if array has negative elements
            long min = arrayLong[0];
            long max = arrayLong[0];

            for (int i = 1; i < array.Length; i++)
            {
                comparisons += 2;
                if (arrayLong[i] < min)
                {
                    min = arrayLong[i];
                }

                if (arrayLong[i] > max)
                {
                    max = arrayLong[i];
                }
            }

            //array contains negative element
            if (min < 0)
            {
                for (int i = 0; i < arrayLong.Length; i++)
                {
                    arrayLong[i] -= min;
                }
                max -= min;
            }

            for (long e = 1; max / e > 0; e *= 10)
            {
                CountingSortByDigits(arrayLong, e, ascending);
            }

            if (min < 0)
            {
                for (int i = 0; i < arrayLong.Length; i++)
                {
                    arrayLong[i] += min;
                }
            }

            for (int i = 0; i < arrayLong.Length; i++)
            {
                array[i] = (float)(arrayLong[i] / Constants.ConvertNumbersWithFloatingPoints);
            }
        }

        private void CountingSortByDigits(long[] array, long exp, bool ascending)
        {
            long[] output = new long[array.Length];
            int[] count = new int[10];

            for (int i = 0; i < array.Length; i++)
            {
                int digits = (int)((array[i] / exp) % 10);
                count[digits]++;
                swaps++;
            }

            if (ascending)
            {
                for (int i = 1; i < 10; i++)
                {
                    count[i] += count[i - 1];
                    swaps++;
                }
            }
            else
            {
                for (int i = 8; i >=0 ; i--)
                {
                    count[i] += count[i + 1];
                    swaps++;
                }
            }

            for (int i = array.Length - 1; i >= 0; i--)
            {
                int digit = (int)((array[i] / exp) % 10);
                output[count[digit] - 1] = array[i];
                count[digit]--;
                swaps++;
            }

            for (int i = 0; i < array.Length; i++)
            {
                array[i] = output[i];
                swaps++;
            }
        }
    }
}
