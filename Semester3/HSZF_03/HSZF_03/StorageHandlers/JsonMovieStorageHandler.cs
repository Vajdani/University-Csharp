using HSZF_03.Interfaces;
using HSZF_03.Models;
using System.Text.Json;

namespace HSZF_03.StorageHandlers
{
    public class JsonMovieStorageHandler : IStorageHandler
    {
        const string _filePath = "movies.json";

        public List<Movie> GetMovies()
        {
            if (!File.Exists(_filePath))
            {
                return [];
            }

            string raw = File.ReadAllText(_filePath);
            return JsonSerializer.Deserialize<List<Movie>>(raw)!;
        }

        public void StoreMovies(List<Movie> movies)
        {
        }
    }
}
