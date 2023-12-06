namespace AdventOfCode_2023.Day2
{
    public static class Day2_1
    {
        public static void Main()
        {
            List<Game> games = ConvertFileToGameSets(@"C:\Users\jwren\Documents\AoC\AOC_2_1.txt");
            GameSubset comparisonSubset = new()
            {
                Blue = 14,
                Green = 13,
                Red = 12
            };
            int sum = 0;
            List<Game> gamesThatGetSummed = games.Where(g => g.GameSubsets.TrueForAll(gs =>
                gs.Blue <= comparisonSubset.Blue
                && gs.Green <= comparisonSubset.Green
                && gs.Red <= comparisonSubset.Red)).ToList();

            foreach (Game game in gamesThatGetSummed)
            {
                sum += game.ID;
            }

            Console.WriteLine(sum);
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
                                break;
                            case "green":
                                gameSubset.Green = qty;
                                break;
                            case "red":
                                gameSubset.Red = qty;
                                break;
                            default:
                                throw new ArgumentException("Parameter did not fill properly", nameof(color));
                        }
                    }

                    game.GameSubsets.Add(gameSubset);
                }

                games.Add(game);
            }

            return games;
        }

        public class Game
        {
            public int ID { get; set; }
            public List<GameSubset> GameSubsets { get; set; } = [];
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
