using System;
using System.Collections.Generic;
using System.Text;
using Variables;


namespace ArrayRelatedFunctions
{
    public static class ArrayManager
    {
        public static float[] CreateArray(int size, float min, float max, GenerationType type)
        {
            Random random = new Random();
            float[] array = new float[size];

            for (int i = 0; i < size; i++)
            {
                array[i] = (float)(random.NextDouble() * (max - min) + min);
            }

            switch (type)
            {
                case GenerationType.Random:
                    break;

                case GenerationType.Ascending:
                    Array.Sort(array);
                    break;

                case GenerationType.Descending:
                    Array.Sort(array);
                    Array.Reverse(array);
                    break;
            }

            return array;
        }
    }
}
