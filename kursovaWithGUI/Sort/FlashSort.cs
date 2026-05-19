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
                    OnStep?.Invoke(array);
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
                    OnStep?.Invoke(array);
                }

                if (i2 >= 0)
                {
                    comparisons++;
                }

                array[i2 + 1] = key;
                swaps++;
                OnStep?.Invoke(array);
            }

        }

        protected override void SortDescending(float[] array)
        {
            if (array.Length == 0) return;

            int m = (int)(0.45 * array.Length);
            if (m <= 0) m = 1;

            int[] l = new int[m];

            float min = array[0];
            int minIndex = 0;
            int maxIndex = 0;

            for (int i = 1; i < array.Length; i++)
            {
                comparisons += 2;
                if (array[i] < min)
                {
                    min = array[i];
                    minIndex = i;
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

            float max = array[maxIndex];
            float c = (m - 1) / (max - min);

            for (int i = 0; i < array.Length; i++)
            {
                int k = (int)(c * (max - array[i]));
                l[k]++;
            }

            for (int t = 1; t < m; t++)
            {
                l[t] += l[t - 1];
            }

            (array[minIndex], array[0]) = (array[0], array[minIndex]);
            swaps++;

            int move = 0;
            int j = 0;
            int classK = m - 1;

            while (move < array.Length - 1)
            {
                while (j > l[classK] - 1)
                {
                    j++;
                    classK = (int)(c * (max - array[j]));
                }

                float flash = array[j];

                while (j != l[classK])
                {
                    classK = (int)(c * (max - flash));
                    int holdIndex = --l[classK];

                    float hold = array[holdIndex];
                    array[holdIndex] = flash;
                    swaps++;
                    flash = hold;
                    move++;
                    OnStep?.Invoke(array);
                }
            }

            for (int i = 1; i < array.Length; i++)
            {
                float key = array[i];
                int i2 = i - 1;

                while ((i2 >= 0) && array[i2] < key)
                {
                    comparisons++;
                    array[i2 + 1] = array[i2];
                    swaps++;
                    i2--;

                    OnStep?.Invoke(array);
                }

                if (i2 >= 0) comparisons++;

                array[i2 + 1] = key;
                swaps++;

                OnStep?.Invoke(array);
            }
        }
    }
}