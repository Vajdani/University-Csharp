using HSZF_09_Practice.BusinessLogic.Interfaces;
using HSZF_09_Practice.Shared.DTOs;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HSZF_09_Practice.WebAPI.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        readonly IMovieService movieService;

        public MovieController(IMovieService movieService) // DI with IoC
        {
            this.movieService = movieService;
        }

        // GET: /<MovieController>/List
        [HttpGet]
        public IEnumerable<MovieListingDTO> List()
        {
            return movieService.GetAllForListing();
        }

        // GET /<MovieController>/Get/5
        [HttpGet("{id}")]
        public MovieCommonDataDTO? Get(int id)
        {
            return movieService.GetCommonDataById(id);
        }

        // POST /<MovieController>/Create
        [HttpPost]
        public void Create([FromBody] MovieCreateDTO movie)
        {
            var model = MovieEditDTO.CreateFrom(movie);
            movieService.Create(model);
        }

        // PUT /<MovieController>/Update
        [HttpPut]
        public void Update([FromBody] MovieEditDTO movie)
        {
            movieService.UpdateMovie(movie);
        }

        // DELETE /<MovieController>/Delete/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            movieService.Delete(id);
        }

        // Custom endpoint
        // /<MovieController>/GetMoviesWithRoles
        [HttpGet]
        public IEnumerable<DetailedMovieDTO> GetMoviesWithRoles()
        {
            return movieService.GetMoviesWithRoles();
        }
    }
}
