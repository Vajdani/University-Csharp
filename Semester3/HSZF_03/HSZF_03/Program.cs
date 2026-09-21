using HSZF_03.Interfaces;
using HSZF_03.Managers;
using HSZF_03.StorageHandlers;
using HSZF_03.Models;

namespace HSZF_03
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IStorageHandler handler = new CsvMovieStorageHandler();
            using MovieManager manager = new(handler);

            manager.MoviesLoaded += Manager_MovieLoaded;
            manager.Display += (s) =>
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(s);
            };

            Display(manager);

            string response;
            do
            {
                Console.Write("Would you like to add a new movie (yes/no): ");

                response = Console.ReadLine()!;
                if (response.ToLower() == "yes")
                {
                    Console.Write("Title: ");
                    string title = Console.ReadLine()!;

                    Console.Write("Length: ");
                    int length = int.Parse(Console.ReadLine()!);

                    Console.Write("Released: ");
                    int released = int.Parse(Console.ReadLine()!);

                    manager.Add(new Movie(title, released, length));
                }
            }
            while (response.ToLower() != "no");

            Console.WriteLine("\nQueries:");
            manager.DisplayOrderedMoviesByLength();
            manager.DisplayMoviesAfter(2000);
        }

        static void Display(MovieManager manager)
        {
            Console.WriteLine("\nAll movies:");
            foreach (Movie movie in manager.GetAll())
            {
                Console.WriteLine(movie);
            }
        }

        static void Manager_MovieLoaded(List<Movie> movie)
        {

        }
    }
}
