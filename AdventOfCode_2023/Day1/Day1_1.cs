namespace AdventOfCode_2023.Day1
{
    public static class Day1_1
    {
        public static void Main()
        {
            List<int> intsToSum = ConvertFileToList(@"C:\Users\jwren\Documents\AoC\AOC_1_1.txt");
            Console.WriteLine(intsToSum.Sum());
        }

        static List<int> ConvertFileToList(string path)
        {
            string[] contents = File.ReadAllLines(path);
            List<int> firstAndLastInts = new();

            foreach (string line in contents)
            {
                string numAsString = string.Empty;

                numAsString += GetFirstDigitAsStringFromString(line);
                numAsString += GetFirstDigitAsStringFromString(Reverse(line));
                firstAndLastInts.Add(int.Parse(numAsString));
            }

            return firstAndLastInts;
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
    }
}
