using HSZF_08_Practice.BusinessLogic.Extensions;
using HSZF_08_Practice.BusinessLogic.Interfaces;
using HSZF_08_Practice.BusinessLogic.Models;
using HSZF_08_Practice.DataAccess.Contexts;
using HSZF_08_Practice.DataAccess.Interfaces;
using HSZF_08_Practice.Shared.Entities;

namespace HSZF_08_Practice.BusinessLogic.Services
{
    public class MovieService : IMovieService
    {
        IMovieRepository movieRepository;
        IRoleRepository roleRepository;
        IDirectorRepository directorRepository;

        public MovieService(IMovieRepository movieRepository, IRoleRepository roleRepository, IDirectorRepository directorRepository)
        {
            this.movieRepository = movieRepository;
            this.roleRepository = roleRepository;
            this.directorRepository = directorRepository;
        }

        public List<Movie> GetAll()
        {
            // TODO: check permission

            return movieRepository.GetAll().ToList();
        }

        public Movie? GetById(int id)
        {
            // TODO: check permission

            return movieRepository.GetById(id);
        }

        public void Create(Movie movie)
        {
            // TODO: check permission

            // Validate data
            Validate(movie);

            var result = movieRepository.Create(movie);
            movie.Id = result.Id; // Update the Movie object with the generated ID

            // TODO: log the creation
        }

        private void Validate(Movie movie)
        {
            if (string.IsNullOrWhiteSpace(movie.Title))
            {
                throw new ArgumentException("Movie title cannot be empty."); // Or collect into a result object
            }

            if (movie.Title.Length > 256)
            {
                throw new ArgumentException("Movie title cannot be longer than 256 characters.");
            }

            // Note: this range is came from the Range attribute in the Movie class, but we can also validate it here
            if (movie.Income < 0 || movie.Income > 10000)
            {
                throw new ArgumentException("Movie income must be between 0 and 10000.");
            }

            if (movie.Rating < 1 || movie.Rating > 10)
            {
                throw new ArgumentException("Movie rating must be between 1 and 10.");
            }

            if (movie.Release.Year < 1888)
            {
                throw new ArgumentException("Movie release year must be greater than 1888.");
            }

            if (!directorRepository.Exists(movie.DirectorId))
            {
                throw new ArgumentException($"Director does not exist with id: {movie.DirectorId}.");
            }
        }

        public void UpdateMovie(Movie movie)
        {
            // TODO: check permission

            // Get existing Movie to ensure it exists before updating
            var existingMovie = movieRepository.GetById(movie.Id);

            if (existingMovie == null)
            {
                throw new ArgumentException("Movie not found.");
            }

            // Copy only the properties that can be updated
            existingMovie.Title = movie.Title;
            existingMovie.Income = movie.Income;
            existingMovie.Rating = movie.Rating;
            existingMovie.Release = movie.Release;
            existingMovie.DirectorId = movie.DirectorId; // We can restrict this if we don't want to allow changing the director

            // Validate data
            Validate(existingMovie);

            var result = movieRepository.Update(existingMovie);

            // TODO: log the update
        }

        public void Delete(int id)
        {
            // TODO: check permission

            movieRepository.Delete(id); // now we can delete the movie without checking if it exists first, as the repository will handle that and throw an exception if it doesn't exist. This is just an example, in a real application we might want to check if the movie exists first and throw a more specific exception.
        }

        /// <summary>
        /// Query 3: Get movies with role numbers over 14 roles, including director name and rating.
        /// </summary>
        /// <returns></returns>
        public List<DetailedMovie> GetMoviesWithRoles()
        {
            var groupedRoles = roleRepository.GetAll().GroupBy(x => x.MovieId).Select(x => new
            {
                MovieId = x.Key,
                RoleNumbers = x.Count(),
            });

            var moviesWithRoleNums = (from movie in movieRepository.GetAll()
                                      join roles in groupedRoles
                                         on movie.Id equals roles.MovieId
                                      where roles.RoleNumbers >= 15
                                      orderby roles.RoleNumbers descending
                                      select new DetailedMovie()
                                      {
                                          Id = movie.Id,
                                          Title = movie.Title,
                                          DirectorName = movie.Director.Name, // Lazy loading!!!
                                          RolesCount = roles.RoleNumbers,
                                          Rating = movie.Rating,
                                      }).ToList();

            return moviesWithRoleNums; // Local variable, but we can return it directly too, but for debugging it's better to have a local variable
        }

        /// <summary>
        /// Query 1: Get films with director data, ordered by release year and then by rating descending.
        /// </summary>
        /// <returns></returns>
        public List<MovieWithDirector> GetFilmsWithDirectorData()
        {
            return GetFilmsWithDirectorDataInternal().ToList();
        }

        /// <summary>
        /// Query 1: Get films with director data, ordered by release year and then by rating descending where it can be filtered by given <paramref name="filter"/> param.
        /// </summary>
        /// <returns></returns>
        public List<MovieWithDirector> GetFilmsWithDirectorData(Predicate<MovieWithDirector> filter)
        {
            return GetFilmsWithDirectorDataInternal().CustomFilterBy(filter).ToList();
        }

        private IOrderedQueryable<MovieWithDirector> GetFilmsWithDirectorDataInternal()
        {
            var movieDetails = from movie in movieRepository.GetAll()
                               join director in directorRepository.GetAll()
                                    on movie.DirectorId equals director.Id
                               select new MovieWithDirector()
                               {
                                   Id = movie.Id,
                                   Title = movie.Title,
                                   Release = movie.Release,
                                   DirectorId = director.Id,
                                   DirectorName = director.Name,
                                   Income = movie.Income,
                                   Rating = movie.Rating,
                               };

            var orderedMovieDetails = movieDetails.OrderBy(x => x.Release).ThenByDescending(x => x.Rating);

            return orderedMovieDetails;
        }
    }
}
