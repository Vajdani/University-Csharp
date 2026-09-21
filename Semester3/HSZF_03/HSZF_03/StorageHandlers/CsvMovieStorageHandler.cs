using HSZF_03.Interfaces;
using HSZF_03.Models;

namespace HSZF_03.StorageHandlers
{
    public class CsvMovieStorageHandler : IStorageHandler
    {
        const string _filePath = "movies.csv";
        const string _delimiter = ",";

        public List<Movie> GetMovies()
        {
            if (!File.Exists(_filePath))
            {
                return [];
            }

            return File.ReadAllLines(_filePath).Select(x =>
            {
                string[] split = x.Split(_delimiter);
                return new Movie(split[0], int.Parse(split[1]), int.Parse(split[2]));
            }).ToList();
        }

        public void StoreMovies(List<Movie> movies)
        {
            string stringContent = string.Join("\n", movies.Select(x => $"{x.Title}{_delimiter}{x.Length}{_delimiter}{x.Released}"));
            File.WriteAllText(_filePath, stringContent);
        }
    }
}
