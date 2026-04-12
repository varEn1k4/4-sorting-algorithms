using ArrayRelatedFunctions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Sort
{
    public abstract class BaseSorter
    {
        protected ulong comparisons;
        protected ulong swaps;

        protected abstract void SortAscending(float[] array);
        protected abstract void SortDescending(float[] array);

        public ResultsAfterSorting Ascending(float[] array)
        {
            comparisons = 0;
            swaps = 0;
            Stopwatch time = Stopwatch.StartNew();

            SortAscending(array);

            time.Stop();

            return new ResultsAfterSorting
            {
                CompareAmount = comparisons,
                SwapsAmount = swaps,
                ExecutionTimeMs = time.Elapsed.TotalMilliseconds
            };
        }

        public ResultsAfterSorting Descending(float[] array)
        {
            comparisons = 0;
            swaps = 0;
            Stopwatch time = Stopwatch.StartNew();

            SortDescending(array);

            time.Stop();

            return new ResultsAfterSorting
            {
                CompareAmount = comparisons,
                SwapsAmount = swaps,
                ExecutionTimeMs = time.Elapsed.TotalMilliseconds
            };
        }
    }
}
