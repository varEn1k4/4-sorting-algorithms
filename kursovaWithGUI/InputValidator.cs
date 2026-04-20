using System;
using System.Collections.Generic;
using System.Text;
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
                && min <= Constants.MaxLimit;
    }

    public static bool TryParseRangeMax(string inputMax, float min, out float max)
    {
        return float.TryParse(inputMax, out max)
                && max >= min + 1
                && max <= Constants.MaxLimit;
    }
}
