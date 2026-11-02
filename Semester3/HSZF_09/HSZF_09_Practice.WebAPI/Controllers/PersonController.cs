using HSZF_09_Practice.BusinessLogic.Interfaces;
using HSZF_09_Practice.Shared.DTOs.Actor;
using HSZF_09_Practice.Shared.DTOs.Director;
using HSZF_09_Practice.Shared.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HSZF_09_Practice.WebAPI.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        readonly IPersonService personService;

        public PersonController(IPersonService personService) // DI with IoC
        {
            this.personService = personService;
        }

        #region Directors

        // GET: /<PersonController>/ListDirectors
        [HttpGet]
        public IEnumerable<DirectorListingDTO> ListDirectors()
        {
            return personService.GetAllForDirectorListing();
        }

        // GET /<PersonController>/GetDirector/5
        [HttpGet("{id}")]
        public DirectorDetailsDTO? GetDirector(int id)
        {
            return personService.GetDirectorDetailsDataById(id);
        }

        // POST /<PersonController>/CreateDirector
        [HttpPost]
        public void CreateDirector([FromBody] DirectorCreateDTO director)
        {
            var model = DirectorEditDTO.CreateFrom(director);
            personService.CreateDirector(model);
        }

        // PUT /<PersonController>/UpdateDirector
        [HttpPut]
        public void UpdateDirector([FromBody] DirectorEditDTO director)
        {
            personService.UpdateDirector(director);
        }

        // DELETE /<PersonController>/DeleteDirector/5
        [HttpDelete("{id}")]
        public void DeleteDirector(int id)
        {
            personService.DeleteDirector(id);
        }

        #endregion

        #region Actors

        // GET: /<PersonController>/ListActors
        [HttpGet]
        public IEnumerable<ActorListingDTO> ListActors()
        {
            return personService.GetAllForActorListing();
        }

        // GET /<PersonController>/GetActor/5
        [HttpGet("{id}")]
        public ActorDetailsDTO? GetActor(int id)
        {
            return personService.GetActorDetailsDataById(id);
        }

        // POST /<PersonController>/CreateActor
        [HttpPost]
        public void CreateActor([FromBody] ActorCreateDTO actor)
        {
            var model = ActorEditDTO.CreateFrom(actor);
            personService.CreateActor(model);
        }

        // PUT /<PersonController>/UpdateActor
        [HttpPut]
        public void UpdateActor([FromBody] ActorEditDTO actor)
        {
            personService.UpdateActor(actor);
        }

        // DELETE /<PersonController>/DeleteActor/5
        [HttpDelete("{id}")]
        public void DeleteActor(int id)
        {
            personService.DeleteActor(id);
        }

        #endregion

        // Custom endpoint
        // /<PersonController>/GetActorPairsInSameMovies
        [HttpGet]
        public IEnumerable<ActorPairs> GetActorPairsInSameMovies()
        {
            return personService.GetActorPairsInSameMovies();
        }

        // Custom endpoint
        // /<PersonController>/GetDirectorStat
        [HttpGet]
        public IEnumerable<DirectorStatDTO> GetDirectorStat()
        {
            return personService.GetDirectorStat();
        }
    }
}
