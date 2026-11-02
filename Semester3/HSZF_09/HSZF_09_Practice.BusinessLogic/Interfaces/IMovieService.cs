using HSZF_09_Practice.BusinessLogic.Models;
using HSZF_09_Practice.Shared.DTOs;
using HSZF_09_Practice.Shared.Entities;

namespace HSZF_09_Practice.BusinessLogic.Interfaces
{
    public interface IMovieService
    {
        void Create(MovieEditDTO movie);
        void Delete(int id);
        List<Movie> GetAll();
        Movie? GetById(int id);
        void UpdateMovie(MovieEditDTO movie);
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
        List<DetailedMovieDTO> GetMoviesWithRoles();
        List<MovieListingDTO> GetAllForListing();
        MovieCommonDataDTO? GetCommonDataById(int id);
    }
}