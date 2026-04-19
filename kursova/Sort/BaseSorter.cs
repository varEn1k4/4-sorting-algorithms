using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Variables;

namespace Sort
{
    public abstract class BaseSorter
    {
        protected ulong comparisons;
        protected ulong swaps;

        protected abstract void SortAscending(float[] array);
        protected abstract void SortDescending(float[] array);

        public bool SortFailded { get; protected set; } = false;

        public ResultsAfterSorting Ascending(float[] array)
        {
            return SortInDirection(array, true);
        }

        public ResultsAfterSorting Descending(float[] array)
        {
            return SortInDirection(array, false);
        }

        private ResultsAfterSorting SortInDirection(float[] array, bool ascending)
        {
            comparisons = 0;
            swaps = 0;
            Stopwatch time = Stopwatch.StartNew();

            if (ascending)
            {
                SortAscending(array);
            }
            else
            {
                SortDescending(array);
            }

            time.Stop();

            return new ResultsAfterSorting(comparisons, swaps, time.Elapsed.TotalMilliseconds);
        }
    }
}
