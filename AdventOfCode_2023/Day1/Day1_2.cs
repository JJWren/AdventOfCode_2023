namespace AdventOfCode_2023.Day1
{
    public static class Day1_2
    {
        public static void Main()
        {
            Console.WriteLine(ConvertFileToSum(@"C:\Users\jwren\Documents\AoC\AOC_1_1.txt"));
        }

        static int ConvertFileToSum(string path)
        {
            string[] contents = File.ReadAllLines(path);
            int totalSum = 0;

            foreach (string line in contents)
            {
                string numAsString = string.Empty;
                string revisedLine = ConvertDigitWordsIntoIntsInString(line, true);

                numAsString += GetFirstDigitAsStringFromString(revisedLine);
                numAsString += GetFirstDigitAsStringFromString(Reverse(revisedLine));
                totalSum += int.Parse(numAsString);
            }

            return totalSum;
        }

        static string GetFirstDigitAsStringFromString(string str)
        {
            foreach (char character in str)
            {
                if (int.TryParse(character.ToString(), out int result))
                {
                    return result.ToString();
                }
            }

            return string.Empty;
        }

        static string Reverse(string str)
        {
            char[] charArray = str.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }

        static string ConvertDigitWordsIntoIntsInString(string str, bool frontToBackOn)
        {
            string[] digits =
            {
                "zero",
                "one",
                "two",
                "three",
                "four",
                "five",
                "six",
                "seven",
                "eight",
                "nine",
            };

            List<DigitAndIndex> digitsAndIndexes = new();

            for (int i = 0; i < digits.Length; i++)
            {
                string digit = digits[i];

                if (str.Contains(digit))
                {
                    DigitAndIndex dai = new()
                    {
                        Digit = digit,
                        DigitIndex = str.IndexOf(digit)
                    };
                    digitsAndIndexes.Add(dai);
                }
            }

            digitsAndIndexes = frontToBackOn
                ? digitsAndIndexes.OrderBy(d => d.DigitIndex).ToList()
                : digitsAndIndexes.OrderByDescending(d => d.DigitIndex).ToList();

            foreach (DigitAndIndex dai in digitsAndIndexes)
            {
                if (str.Contains(dai.Digit))
                {
                    string intReplacement = ConvertDigitToIntStr(dai.Digit);
                    str = string.Join(intReplacement, str.Split(dai.Digit));
                }
            }

            return str;
        }

        static string ConvertDigitToIntStr(string digit)
        {
            switch (digit)
            {
                case "zero":
                    return "z0o";
                case "one":
                    return "o1e";
                case "two":
                    return "t2o";
                case "three":
                    return "t3e";
                case "four":
                    return "f4r";
                case "five":
                    return "f5e";
                case "six":
                    return "s6x";
                case "seven":
                    return "s7n";
                case "eight":
                    return "e8t";
                case "nine":
                    return "n9e";
                default:
                    throw new ArgumentException("Parameter must be a digit", nameof(digit));
            }
        }

        public class DigitAndIndex
        {
            public string Digit { get; set; } = string.Empty;
            public int DigitIndex { get; set; }
        }
    }
}
