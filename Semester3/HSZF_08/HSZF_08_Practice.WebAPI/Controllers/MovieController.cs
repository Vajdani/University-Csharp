using HSZF_08_Practice.BusinessLogic.Interfaces;
using HSZF_08_Practice.Shared.Entities;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HSZF_08_Practice.WebAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        IMovieService service;

        public MovieController(IMovieService service)
        {
            this.service = service;
        }

        // GET: api/<MovieController>
        [HttpGet]
        public IEnumerable<Movie> Get()
        {
            return service.GetAll();
        }

        // GET api/<MovieController>/5
        [HttpGet("{id}")]
        public Movie? Get(int id)
        {
            return service.GetById(id);
        }

        // POST api/<MovieController>
        [HttpPost]
        public void Post([FromBody] Movie movie)
        {
            service.Create(movie);
        }

        // PUT api/<MovieController>/5
        [HttpPut]
        public void Put([FromBody] Movie movie)
        {
            service.UpdateMovie(movie);
        }

        // DELETE api/<MovieController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            service.Delete(id);
        }
    }
}
