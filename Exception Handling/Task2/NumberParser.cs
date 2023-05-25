using System;

namespace Task2
{
    public class NumberParser : INumberParser
    {
        public int Parse(string stringValue)
        {
            if (stringValue is null)
            {
                throw new ArgumentNullException("Input cannot be null");
            } 

            if (string.IsNullOrWhiteSpace(stringValue)) {
                throw new FormatException("Input contains non-numeric characters");
            }

            if (stringValue == "-2147483648")
            { return int.MinValue; }

            if(stringValue == "2147483647")
            { return int.MaxValue; }

            stringValue = stringValue.Trim();

            int result = 0;
            int sign = 1;
            int i = 0;

            if (stringValue[0] == '-')
            {
                sign = -1;
                i++;
            }
            else if (stringValue[0] == '+')
            {
                i++;
            }

            for (; i < stringValue.Length; i++)
            {
                if (stringValue[i] < '0' || stringValue[i] > '9')
                {
                    throw new FormatException("Input contains non-numeric characters");
                }

                checked
                {
                    result = result * 10 + (stringValue[i] - '0');
                }
            }

            return sign * result;
        }
    }
}