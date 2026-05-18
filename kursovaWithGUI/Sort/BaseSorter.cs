using System.Diagnostics;
using Variables;

namespace Sort
{
    public abstract class BaseSorter
    {
        protected ulong comparisons;
        protected ulong swaps;

        public Action<float[]> OnStep { get; set; }
        protected abstract void SortAscending(float[] array);
        protected abstract void SortDescending(float[] array);

        public ResultsAfterSorting Ascending(float[] array)
        {
            return Sort(array, true);
        }

        public ResultsAfterSorting Descending(float[] array)
        {
            return Sort(array, false);
        }

        private ResultsAfterSorting Sort(float[] array, bool ascending)
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
