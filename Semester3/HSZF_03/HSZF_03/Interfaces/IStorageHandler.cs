using HSZF_03.Models;

namespace HSZF_03.Interfaces
{
    public interface IStorageHandler
    {
        List<Movie> GetMovies();
        void StoreMovies(List<Movie> movies);
    }
}
