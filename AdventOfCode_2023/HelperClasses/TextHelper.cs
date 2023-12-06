using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode_2023.HelperClasses
{
    public static class TextHelper
    {
        public static string ReadInputAndAddBanner(string path)
        {
            string[] contents = File.ReadAllLines(path);
            string bannerfiedContents = string.Empty;
            int topAndBottomBannerLength = contents[0].Length + 2;
            string topAndBottom = string.Join("", Enumerable.Repeat('.', topAndBottomBannerLength));

            bannerfiedContents += $"{topAndBottom}\n";

            for (int i = 0; i < contents.Length; i++)
            {
                bannerfiedContents += $".{contents[i]}.\n";
            }

            bannerfiedContents += $"{topAndBottom}\n";

            return bannerfiedContents;
        }

        public static void CreateTxtFileFromStringViaNewLines(string str, string destinationPath)
        {
            string[] lines = str.Split("\n");

            using (StreamWriter outputFile = new StreamWriter(destinationPath))
            {
                foreach (string line in lines)
                    outputFile.WriteLine(line);
            }
        }
    }
}
