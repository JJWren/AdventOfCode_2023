using System.Text.RegularExpressions;

namespace AdventOfCode_2023.Day3
{
    public static class Day3_1
    {
        public static void Main()
        {
            // I changed the puzzle input to have a "box" of dots around the whole input to avoid limit errors on loops.

            List<Number> partNumbers = ReadFileAndGetPartNumbers(@"C:\Users\jwren\Documents\AoC\AoC_3_1_Fixed.txt");
            SumAndPrintPartNumbers(partNumbers);
        }

        const string digitsRegex = @"[0-9]";
        const string nonSymbolsRegex = @"[.0-9]";

        public static void SumAndPrintPartNumbers(List<Number> partNumbers)
        {
            int sumOfPartNumbers = 0;

            foreach (Number partNumber in partNumbers)
            {
                sumOfPartNumbers += partNumber.Value;
            }

            Console.WriteLine(sumOfPartNumbers);
        }

        public static List<Number> ReadFileAndGetPartNumbers(string path)
        {
            string[] lines = File.ReadAllLines(path);
            List<Number> partNumbers = new();

            for (int i = 0; i < lines.Length; i++)
            {
                for (int j = 0; j < lines[i].Length; j++)
                {
                    if (Regex.IsMatch(lines[i][j].ToString(), digitsRegex))
                    {
                        List<int> numberPositions = new();
                        (string numberStr, numberPositions) = CheckNextPositionIsDigit(lines[i], j, lines[i]);

                        Number number = new()
                        {
                            Value = int.Parse(numberStr),
                            XPositions = numberPositions,
                            YIdx = i
                        };

                        number.SetNumberIsPartNumber(lines);

                        if (number.IsPartNumber)
                            partNumbers.Add(number);

                        j += numberStr.Length;
                    }
                }
            }

            return partNumbers;
        }

        public static (string, List<int>) CheckNextPositionIsDigit(string str, int idx, string line)
        {
            string number = string.Empty;
            List<int> numberPositions = new();
            bool hasHitEndOfNumber = false;

            while (!hasHitEndOfNumber)
            {
                if (!int.TryParse(line.ElementAt(idx).ToString(), out int result))
                {
                    hasHitEndOfNumber = true;
                    break;
                }
                else
                {
                    numberPositions.Add(idx);
                    number += result.ToString();
                    idx++;
                }
            }

            return (number, numberPositions);
        }

        public class SymbolCoordinate
        {
            public int X { get; set; } = 0;
            public int Y { get; set; } = 0;
        }

        public class Number
        {
            public int Value { get; set; } = 0;
            public List<int> XPositions { get; set; } = new();
            public int YIdx { get; set; } = 0;
            public bool IsPartNumber { get; set; } = false;

            public void SetNumberIsPartNumber(string[] strArr)
            {
                int lastXIndex = XPositions[XPositions.Count() - 1];
                if (!Regex.IsMatch(strArr[YIdx][XPositions[0] - 1].ToString(), nonSymbolsRegex)
                    || !Regex.IsMatch(strArr[YIdx][lastXIndex + 1].ToString(), nonSymbolsRegex))
                {
                    IsPartNumber = true;
                    return;
                }

                foreach (int xCoord in XPositions)
                {
                    if (!Regex.IsMatch(strArr[YIdx - 1][xCoord - 1].ToString(), nonSymbolsRegex)
                        || !Regex.IsMatch(strArr[YIdx - 1][xCoord].ToString(), nonSymbolsRegex)
                        || !Regex.IsMatch(strArr[YIdx - 1][xCoord + 1].ToString(), nonSymbolsRegex)
                        || !Regex.IsMatch(strArr[YIdx + 1][xCoord - 1].ToString(), nonSymbolsRegex)
                        || !Regex.IsMatch(strArr[YIdx + 1][xCoord].ToString(), nonSymbolsRegex)
                        || !Regex.IsMatch(strArr[YIdx + 1][xCoord + 1].ToString(), nonSymbolsRegex))
                    {
                        IsPartNumber = true;
                        return;
                    }
                }
            }
        }
    }
}
