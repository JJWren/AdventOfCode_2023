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
    public static partial class Day4_2
    {
        public static void Main()
        {
            Console.WriteLine(DetermineTotalScratchcards(ReadFileGenerateCards(@"C:\Users\jwren\Documents\AoC\AoC_4_1.txt", false)));
        }

        public static List<Card> ReadFileGenerateCards(string path, bool generateOnlyWinningCards)
        {
            string[] contents = File.ReadAllLines(path);
            List<Card> cards = [];

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
                cards.Add(card);
            }

            return generateOnlyWinningCards ? GetWinningCards(cards) : cards;
        }

        public static int DetermineTotalScratchcards(List<Card> cards)
        {
            int count = 0;

            foreach(Card card in cards)
            {
                Console.WriteLine($"\nStarting {card.ID}...");
                count++;

                if (card.TotalPoints > 0)
                {
                    Console.WriteLine($"Current card instances: {card.InstancesOfCard}");
                    Console.WriteLine("Setting new instances of next card batch...");
                    int instances = card.InstancesOfCard;

                    while (instances > 0)
                    {
                        count++;
                        int totalWinningNumbers = card.GetWinningNumbersTotal();
                        IncreaseNextXSetOfCardsInstances(cards, card.ID, totalWinningNumbers);
                        instances--;
                    }
                }
            }

            return count;
        }

        public static List<Card> GetWinningCards(List<Card> cards)
        {
            return cards.Where(c => c.TotalPoints > 0).ToList();
        }

        public static void IncreaseNextXSetOfCardsInstances(List<Card> cards, int currentCardID, int x)
        {
            while (x > 0)
            {
                int potentialCardID = currentCardID + x;

                if (cards.FirstOrDefault(c => c.ID == potentialCardID) != null)
                {
                    cards.First(c => c.ID == potentialCardID).InstancesOfCard++;
                }

                x--;
            }
        }

        public class Card
        {
            public int ID { get; set; }
            public List<int> UserNumbers { get; set; } = [];
            public List<int> ScorableNumbers { get; set; } = [];
            public int TotalPoints { get; set; } = 0;
            public int InstancesOfCard { get; set; } = 1;

            public void SetTotalPoints()
            {
                List<int> matchedNumbers = GetWinningNumbers();

                if (matchedNumbers.Count > 0)
                {
                    TotalPoints = (int)Math.Pow(2, matchedNumbers.Count - 1);
                }
            }

            public List<int> GetWinningNumbers()
            {
                return UserNumbers.Intersect(ScorableNumbers).ToList();
            }

            public int GetWinningNumbersTotal()
            {
                return GetWinningNumbers().Count;
            }
        }
    }
}
