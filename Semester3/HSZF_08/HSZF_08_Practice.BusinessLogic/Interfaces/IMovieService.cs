using HSZF_08_Practice.BusinessLogic.Models;
using HSZF_08_Practice.Shared.Entities;

namespace HSZF_08_Practice.BusinessLogic.Interfaces
{
    public interface IMovieService
    {
        void Create(Movie movie);
        void Delete(int id);
        List<Movie> GetAll();
        Movie? GetById(int id);
        void UpdateMovie(Movie movie);
        /// <summary>
        /// Query 1: Get films with director data, ordered by release year and then by rating descending.
        /// </summary>
        /// <returns></returns>
        List<MovieWithDirector> GetFilmsWithDirectorData();
        /// <summary>
        /// Query 1: Get films with director data, ordered by release year and then by rating descending where it can be filtered by given <paramref name="filter"/> param.
        /// </summary>
        /// <returns></returns>
        List<MovieWithDirector> GetFilmsWithDirectorData(Predicate<MovieWithDirector> filter);
        /// <summary>
        /// Query 3: Get movies with role numbers over 14 roles, including director name and rating.
        /// </summary>
        /// <returns></returns>
        List<DetailedMovie> GetMoviesWithRoles();
    }
}