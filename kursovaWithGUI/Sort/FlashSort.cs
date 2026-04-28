using Sort;

namespace SortAlgorithms
{
    public class FlashSort : BaseSorter
    {
        protected override void SortAscending(float[] array)
        {
            if (array.Length == 0)
            {
                return;
            }

            int m = (int)(0.45 * array.Length);
            if (m <= 0)
            {
                m = 1;
            }

            int[] l = new int[m];

            float min = array[0];
            int maxIndex = 0;

            for (int i = 1; i < array.Length; i++)
            {
                comparisons += 2;
                if (array[i] < min)
                {
                    min = array[i];
                }

                if (array[i] > array[maxIndex])
                {
                    maxIndex = i;
                }
            }

            if (min == array[maxIndex])
            {
                return;
            }

            float c = (m - 1) / (array[maxIndex] - min);

            for (int i = 0; i < array.Length; i++)
            {
                int k = (int)(c * (array[i] - min));
                l[k]++;
            }

            for (int t = 1; t < m; t++)
            {
                l[t] += l[t - 1];
            }

            (array[maxIndex], array[0]) = (array[0], array[maxIndex]);
            swaps++;

            int move = 0;
            int j = 0;
            int classK = m - 1;

            while (move < array.Length - 1)
            {
                while (j > l[classK] - 1)
                {
                    j++;
                    classK = (int)(c * (array[j] - min));
                }

                float flash = array[j];

                while (j != l[classK])
                {
                    classK = (int)(c * (flash - min));
                    int holdIndex = --l[classK];

                    float hold = array[holdIndex];
                    array[holdIndex] = flash;
                    swaps++;
                    flash = hold;
                    move++;
                }
            }

            for (int i = 1; i < array.Length; i++)
            {
                float key = array[i];
                int i2 = i - 1;

                while ((i2 >= 0) && array[i2] > key)
                {
                    comparisons++;
                    array[i2 + 1] = array[i2];
                    swaps++;
                    i2--;
                }

                if (i2 >= 0)
                {
                    comparisons++;
                }

                array[i2 + 1] = key;
                swaps++;
            }

        }


        protected override void SortDescending(float[] array)
        {
            SortAscending(array);

            int left = 0;
            int right = array.Length - 1;
            while (left < right)
            {
                (array[left], array[right]) = (array[right], array[left]);
                swaps += 3;
                left++;
                right--;
            }
        }
    }
}