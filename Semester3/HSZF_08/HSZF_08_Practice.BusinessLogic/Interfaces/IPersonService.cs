using HSZF_08_Practice.BusinessLogic.Models;
using HSZF_08_Practice.Shared.Entities;

namespace HSZF_08_Practice.BusinessLogic.Interfaces
{
    public interface IPersonService
    {
        #region Director CRUD

        List<Director> GetAllDirectors();
        Director? GetDirectorById(int id);
        void CreateDirector(Director director);
        void UpdateDirector(Director director);
        void DeleteDirector(int id);

        #endregion

        #region Actor CRUD

        List<Actor> GetAllActors();
        Actor? GetActorById(int id);
        void CreateActor(Actor actor);
        void UpdateActor(Actor actor);
        void DeleteActor(int id);

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
        List<DirectorStat> GetDirectorStat();
    }
}