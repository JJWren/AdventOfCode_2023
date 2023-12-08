using AdventOfCode_2023.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode_2023.Day4
{
    public static partial class Day4_1
    {
        public static void Main()
        {
            Console.WriteLine(ReadFileGenerateWinningCards(@"C:\Users\jwren\Documents\AoC\AoC_4_1.txt").Sum(c => c.TotalPoints));
        }

        public static List<Card> ReadFileGenerateWinningCards(string path)
        {
            string[] contents = File.ReadAllLines(path);
            List<Card> winningCards = [];

            for (int i = 0; i < contents.Length; i++)
            {
                Card card = new();
                string line = contents[i].ReplaceWhitespaceWithSingleSpace();
                string[] cardVsNumbers = line.Split(":");
                card.ID = int.Parse(cardVsNumbers[0].Split(" ")[1]);
                string userNumbers = cardVsNumbers[1].Split("|")[0].Trim();
                string scorableNumbers = cardVsNumbers[1].Split("|")[1].Trim();
                card.UserNumbers = userNumbers.Split(" ").Select(int.Parse).ToList();
                card.ScorableNumbers = scorableNumbers.Split(" ").Select(int.Parse).ToList();
                card.SetTotalPoints();
                
                if (card.TotalPoints > 0)
                {
                    winningCards.Add(card);
                }
            }

            return winningCards;
        }

        public class Card
        {
            public int ID { get; set; }
            public List<int> UserNumbers { get; set; } = [];
            public List<int> ScorableNumbers { get; set; } = [];
            public int TotalPoints { get; set; } = 0;

            public void SetTotalPoints ()
            {
                List<int> matchedNumbers = UserNumbers.Intersect(ScorableNumbers).ToList();

                if (matchedNumbers.Count > 0 )
                {
                    TotalPoints = (int)Math.Pow(2, matchedNumbers.Count - 1);
                }
            }
        }
    }
}
