namespace PMP_F1_ZH1
{
    internal class Program
    {
        public static string[] genres;
        static List<Game> games = new();
        static void Main(string[] args)
        {
            StreamReader reader = new("../../../data/genre.txt");
            string genreText = reader.ReadToEnd();
            reader.Close();

            string[] genreSplit = genreText.Split(",");
            genres = new string[genreSplit.Length];
            foreach (string genre in genreSplit)
            {
                string[] data = genre.Split("=");
                genres[int.Parse(data[1])] = data[0];
            }

            reader = new("../../../data/games_dataset.csv");
            reader.ReadLine();
            while(!reader.EndOfStream)
            {
                games.Add(new Game(reader.ReadLine()));
            }
            reader.Close();

            Console.Write("3. feladat: Adott kiadóhoz tartozó játékok száma: ");
            string publisher = Console.ReadLine();
            int gameCount = 0;
            foreach (Game game in games)
            {
                if (game.Publisher == publisher)
                {
                    gameCount++;
                }
            }
            Console.WriteLine($"A kiadó által piacra dobott játékok száma: {gameCount}");

            Console.WriteLine("\n4. feladat: A megjelenés napjától elérhető játékok");
            foreach (Game game in games)
            {
                if (game.OriginalReleaseDate == game.PlatformReleaseDate)
                {
                    Console.WriteLine($"{game.Title} | {game.Genre} | {game.OriginalReleaseDate}");
                }
            }

            Console.WriteLine("\n5. feladat: Játékok száma műfajonként");
            Dictionary<string, int> genreCount = new();
            foreach (string genre in genres)
            {
                genreCount.Add(genre, 0);
            }

            foreach (Game game in games)
            {
                genreCount[game.Genre]++;
            }

            foreach (var item in genreCount)
            {
                Console.WriteLine($"{item.Key}: {item.Value}");
            }
        }
    }
}
