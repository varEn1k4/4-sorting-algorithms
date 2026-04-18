using Sort;
using System;
using System.Collections.Generic;
using System.Text;

namespace SortAlgorithms
{ 
    public class BucketSort : BaseSorter
    {
        protected override void SortAscending(float[] array)
        {
            BucketSortMain(array, true);
        }

        protected override void SortDescending(float[] array)
        {
            BucketSortMain(array, false);
        }

        private void BucketSortMain(float[] array, bool ascending)
        {
            if (array.Length == 0)
            {
                return;
            }

            float min = array[0];
            float max = array[0];

            for (int i = 0; i < array.Length; i++)
            {
                comparisons += 2;

                if (array[i] > max)
                {
                    max = array[i];
                }

                if (array[i] < min)
                {
                    min = array[i];
                }
            }

            if (max == min)
            {
                return;
            }

            int bucketCount = array.Length;
            List<float>[] buckets = new List<float>[bucketCount];

            for (int i = 0; i < bucketCount; i++)
            {
                buckets[i] = new List<float>();
            }

            for (int i = 0; i < array.Length; i++)
            {
                int bucketIndex = (int)((array[i] - min) / (max - min) * (bucketCount - 1));
                buckets[bucketIndex].Add(array[i]);
                swaps++;
            }

            int currentIndex = 0;

            if (ascending)
            {
                for (int i = 0; i < bucketCount; i++)
                {
                    if (buckets[i].Count > 0)
                    {
                        InsertionSortBucket(buckets[i], ascending);

                        foreach (float item in buckets[i])
                        {
                            array[currentIndex++] = item;
                            swaps++;
                        }
                    }
                }
            }
            else
            {
                for (int i = bucketCount - 1; i >= 0; i--)
                {
                    if (buckets[i].Count > 0)
                    {
                        InsertionSortBucket(buckets[i], ascending);

                        foreach (float item in buckets[i])
                        {
                            array[currentIndex++] = item;
                            swaps++;
                        }
                    }
                }
            }
            
        }

        private void InsertionSortBucket(List<float> bucket, bool ascending)
        {
            for (int i = 1; i < bucket.Count; i++)
            {
                float key = bucket[i];
                int j = i - 1;

                if (ascending)
                {
                    while (j >= 0 && bucket[j] > key)
                    {
                        comparisons++;
                        bucket[j + 1] = bucket[j];
                        swaps++;
                        j--;
                    }
                    if (j >= 0)
                    {
                        comparisons++;
                    }
                }
                else
                {
                    while (j >= 0 && bucket[j] < key)
                    {
                        comparisons++;
                        bucket[j + 1] = bucket[j];
                        swaps++;
                        j--;
                    }
                    if (j >= 0)
                    {
                        comparisons++;
                    }
                }
                bucket[j + 1] = key;
                swaps++;
            }
        }
    }
}
