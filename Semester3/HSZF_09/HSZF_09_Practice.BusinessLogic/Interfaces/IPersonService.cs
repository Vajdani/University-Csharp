using HSZF_09_Practice.Shared.DTOs.Actor;
using HSZF_09_Practice.Shared.DTOs.Director;
using HSZF_09_Practice.Shared.Entities;
using HSZF_09_Practice.Shared.Models;

namespace HSZF_09_Practice.BusinessLogic.Interfaces
{
    public interface IPersonService
    {
        #region Director CRUD

        List<Director> GetAllDirectors();
        Director? GetDirectorById(int id);
        void CreateDirector(DirectorEditDTO director);
        void UpdateDirector(DirectorEditDTO director);
        void DeleteDirector(int id);
        List<DirectorListingDTO> GetAllForDirectorListing();
        DirectorDetailsDTO? GetDirectorDetailsDataById(int id);

        #endregion

        #region Actor CRUD

        List<Actor> GetAllActors();
        Actor? GetActorById(int id);
        void CreateActor(ActorEditDTO actor);
        void UpdateActor(ActorEditDTO actor);
        void DeleteActor(int id);
        List<ActorListingDTO> GetAllForActorListing();
        ActorDetailsDTO? GetActorDetailsDataById(int id);

        #endregion

        /// <summary>
        /// Query 5: Get actor pairs who have acted in at least 2 common movies, ordered by first actor name and then second actor name.
        /// </summary>
        /// <returns></returns>
        List<ActorPairs> GetActorPairsInSameMovies();

        /// <summary>
        /// Query 4: Get director statistics for directors who have directed at least 2 movies, including the number of movies, average rating, total income, and highest income.
        /// </summary>
        /// <returns></returns>
        List<DirectorStatDTO> GetDirectorStat();
    }
}