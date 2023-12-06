namespace AdventOfCode_2023.Day2
{
    public static class Day2_2
    {
        public static void Main()
        {
            List<Game> games = ConvertFileToGameSets(@"C:\Users\jwren\Documents\AoC\AOC_2_1.txt");

            Console.WriteLine(games.Sum(g => g.Power));
        }

        public static List<Game> ConvertFileToGameSets(string path)
        {
            string[] contents = File.ReadAllLines(path);
            List<Game> games = new();

            foreach (string line in contents)
            {
                Game game = new();
                string[] gameAndContents = line.Split(":");

                game.ID = int.Parse(gameAndContents[0].Split(" ")[1]);

                string[] subsets = gameAndContents[1].Split(";");
                for (int i = 0; i < subsets.Length; i++)
                {
                    string subset = subsets[i];
                    GameSubset gameSubset = new()
                    {
                        ID = i + 1
                    };
                    string[] qtysAndColors = subset.Split(",");

                    foreach (string qtyAndColor in qtysAndColors)
                    {
                        string[] qtyAndColorSplit = qtyAndColor.Trim().Split(" ");
                        int qty = int.Parse(qtyAndColorSplit[0]);
                        string color = qtyAndColorSplit[1];

                        switch (color)
                        {
                            case "blue":
                                gameSubset.Blue = qty;
                                game.HighestBlue = (qty > game.HighestBlue) ? qty : game.HighestBlue;
                                break;
                            case "green":
                                gameSubset.Green = qty;
                                game.HighestGreen = (qty > game.HighestGreen) ? qty : game.HighestGreen;
                                break;
                            case "red":
                                gameSubset.Red = qty;
                                game.HighestRed = (qty > game.HighestRed) ? qty : game.HighestRed;
                                break;
                            default:
                                throw new ArgumentException("Parameter did not fill properly", nameof(color));
                        }
                    }

                    game.GameSubsets.Add(gameSubset);
                }

                game.Power = game.HighestBlue * game.HighestGreen * game.HighestRed;
                games.Add(game);
            }

            return games;
        }

        public class Game
        {
            public int ID { get; set; }
            public List<GameSubset> GameSubsets { get; set; } = new();
            public int HighestBlue { get; set; } = 0;
            public int HighestGreen { get; set; } = 0;
            public int HighestRed { get; set; } = 0;
            public int Power { get; set; } = 0;
        }

        public class GameSubset
        {
            public int ID { get; set; }
            public int Blue { get; set; } = 0;
            public int Green { get; set; } = 0;
            public int Red { get; set; } = 0;
        }
    }
}
