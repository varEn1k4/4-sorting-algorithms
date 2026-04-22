using Variables;

public static class InputValidator
{
    public static bool TryParseSize(string input, out int size)
    {
        bool isSizeOfArrayCorrect = int.TryParse(input, out size)
                && size >= Constants.MinArraySize
                && size <= Constants.MaxArraySize;

        if (!isSizeOfArrayCorrect)
        {
            size = 0;
            return false;
        }

        return true;
    }

    public static bool TryParseRangeMin(string inputMin, out float min)
    {
        return float.TryParse(inputMin, out min)
                && min >= Constants.MinLimit
                && min <= Constants.MaxLimit - 1;
    }

    public static bool TryParseRangeMax(string inputMax, float min, out float max)
    {
        return float.TryParse(inputMax, out max)
                && max >= min + 1
                && max <= Constants.MaxLimit;
    }

    public static int GetDynamicMultiplier(float[] array)
    {
        int maxDecimalPlaces = 0;

        foreach(float num in array)
        {
            string stringNumber = num.ToString("0.#####", System.Globalization.CultureInfo.InvariantCulture);

            int dotIndex = stringNumber.IndexOf('.');

            if (dotIndex != -1)
            {
                int currenPlaces = stringNumber.Length - dotIndex - 1;

                if (currenPlaces > maxDecimalPlaces)
                {
                    maxDecimalPlaces = currenPlaces;
                }
            }

            if (maxDecimalPlaces == 5)
            {
                break;
            }
        }

        return (int)Math.Pow(10, maxDecimalPlaces);
    }
}
