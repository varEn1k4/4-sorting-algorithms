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
            Dictionary<long, int> counts = new Dictionary<long, int>(array.Length);

            for (int i = 0; i < array.Length; i++)
            {
                long val = (long)Math.Round(array[i] * multiplier);

                if (counts.ContainsKey(val))
                {
                    counts[val]++;
                }
                else
                {
                    counts[val] = 1;
                }

                comparisons++;
            }

            List<long> uniqueKeys = counts.Keys.ToList();
            uniqueKeys.Sort();

            if (!ascending)
            {
                uniqueKeys.Reverse();
            }

            int index = 0;
            foreach (long key in uniqueKeys)
            {
                int count = counts[key];

                for (int i = 0; i < count; i++)
                {
                    array[index++] = (float)Math.Round(key / (double)multiplier, 5);
                    swaps++; 

                    OnStep?.Invoke(array);
                }
            }
        }
    }
}
