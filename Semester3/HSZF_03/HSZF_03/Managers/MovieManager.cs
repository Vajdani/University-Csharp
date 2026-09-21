using HSZF_03.Interfaces;
using HSZF_03.Models;

namespace HSZF_03.Managers
{
    public class MovieManager : IDisposable
    {
        List<Movie> movies;
        IStorageHandler storage;

        public event Action<List<Movie>> MoviesLoaded;
        public event Action<List<Movie>> MovieStored;
        public event Action<Movie> MovieAdded;
        public event Action<string> Display;

        public MovieManager(IStorageHandler storage)
        {
            this.storage = storage;
            this.movies = storage.GetMovies();

            if (MoviesLoaded != null)
            {
                MoviesLoaded(movies);
            }
        }

        public void Dispose()
        {
            storage.StoreMovies(this.movies);
            MovieStored?.Invoke(movies);
        }

        public List<Movie> GetAll()
        {
            return movies;
        }

        public void Add(Movie movie)
        {
            movies.Add(movie);
        }

        public void DisplayOrderedMoviesByLength()
        {
            Display?.Invoke("\nQuery #1: Ordered by length:");
            foreach (Movie movie in movies.OrderBy(x => x.Length))
            {
                Display?.Invoke(movie.ToString());
            }
        }

        public void DisplayMoviesAfter(int year)
        {
            Display?.Invoke($"\nQuery #2: Movies after {year}:");
            foreach (Movie movie in movies.Where(x => x.Released >= year).OrderByDescending(x => x.Released))
            {
                Display?.Invoke(movie.ToString());
            }
        }
    }
}
