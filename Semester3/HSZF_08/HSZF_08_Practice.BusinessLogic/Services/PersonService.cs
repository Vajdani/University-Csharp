using HSZF_08_Practice.BusinessLogic.Interfaces;
using HSZF_08_Practice.BusinessLogic.Models;
using HSZF_08_Practice.DataAccess.Interfaces;
using HSZF_08_Practice.Shared.Entities;

namespace HSZF_08_Practice.BusinessLogic.Services
{
    public class PersonService : IPersonService
    {
        IDirectorRepository directorRepository;
        IActorRepository actorRepository;
        IRoleRepository roleRepository;

        public PersonService(IDirectorRepository directorRepository, IActorRepository actorRepository, IRoleRepository roleRepository) // Dependency Injection
        {
            this.directorRepository = directorRepository;
            this.actorRepository = actorRepository;
            this.roleRepository = roleRepository;
        }

        #region Director CRUD

        public List<Director> GetAllDirectors()
        {
            // TODO: check permission

            return directorRepository.GetAll().ToList();
        }

        public Director? GetDirectorById(int id)
        {
            // TODO: check permission

            return directorRepository.GetById(id);
        }

        public void CreateDirector(Director director)
        {
            // TODO: check permission

            // TODO: validate director data
            if (string.IsNullOrWhiteSpace(director.Name))
            {
                throw new ArgumentException("Director name cannot be empty."); // Or collect into a result object
            }

            var result = directorRepository.Create(director);
            director.Id = result.Id; // Update the director object with the generated ID

            // TODO: log the creation
        }

        public void UpdateDirector(Director director)
        {
            // TODO: check permission

            // Get existing director to ensure it exists before updating
            var existingDirector = directorRepository.GetById(director.Id);

            if (existingDirector == null)
            {
                throw new ArgumentException("Director not found.");
            }

            // Copy only the properties that can be updated
            existingDirector.Name = director.Name;

            // TODO: validate director data
            if (string.IsNullOrWhiteSpace(existingDirector.Name))
            {
                throw new ArgumentException("Director name cannot be empty."); // Or collect into a result object
            }

            var result = directorRepository.Update(existingDirector);

            // TODO: log the update
        }

        public void DeleteDirector(int id)
        {
            // TODO: check permission

            if (!directorRepository.Exists(id))
            {
                throw new ArgumentException("Director not found.");
            }

            directorRepository.Delete(id);

            // TODO: log the deletion
        }

        #endregion

        #region Actor CRUD

        public List<Actor> GetAllActors()
        {
            // TODO: check permission

            return actorRepository.GetAll().ToList();
        }

        public Actor? GetActorById(int id)
        {
            // TODO: check permission

            return actorRepository.GetById(id);
        }

        public void CreateActor(Actor actor)
        {
            // TODO: check permission

            // TODO: validate Actor data
            if (string.IsNullOrWhiteSpace(actor.Name))
            {
                throw new ArgumentException("Actor name cannot be empty."); // Or collect into a result object
            }

            var result = actorRepository.Create(actor);
            actor.Id = result.Id; // Update the Actor object with the generated ID

            // TODO: log the creation
        }

        public void UpdateActor(Actor actor)
        {
            // TODO: check permission

            // Get existing Actor to ensure it exists before updating
            var existingActor = actorRepository.GetById(actor.Id);

            if (existingActor == null)
            {
                throw new ArgumentException("Actor not found.");
            }

            // Copy only the properties that can be updated
            existingActor.Name = actor.Name;

            // TODO: validate Actor data
            if (string.IsNullOrWhiteSpace(existingActor.Name))
            {
                throw new ArgumentException("Actor name cannot be empty."); // Or collect into a result object
            }

            var result = actorRepository.Update(existingActor);

            // TODO: log the update
        }

        public void DeleteActor(int id)
        {
            // TODO: check permission

            if (!actorRepository.Exists(id))
            {
                throw new ArgumentException("Actor not found.");
            }

            actorRepository.Delete(id);

            // TODO: log the deletion
        }

        #endregion

        /// <summary>
        /// Query 5: Get actor pairs who have acted in at least 2 common movies, ordered by first actor name and then second actor name.
        /// </summary>
        /// <returns></returns>
        public List<ActorPairs> GetActorPairsInSameMovies()
        {
            var actorPairs = from actor1 in roleRepository.GetAll()
                             join actor2 in roleRepository.GetAll()
                                on actor1.MovieId equals actor2.MovieId
                             where actor1.Id < actor2.Id // This is necessary for prevent row duplication A - B vs B - A
                             select new
                             {
                                 Act1Name = actor1.Actor.Name,
                                 Act2Name = actor2.Actor.Name,
                                 MovieId = actor1.MovieId
                             };


            var groupedActorPairs = from actors in actorPairs
                                    group actors by new { actors.Act1Name, actors.Act2Name } into grouped
                                    select new ActorPairs
                                    {
                                        FirstActorName = grouped.Key.Act1Name,
                                        SecondActorName = grouped.Key.Act2Name,
                                        CommonMoviesCount = grouped.Count()
                                    };

            var filteredOrderedActorPairs = groupedActorPairs.Where(x => x.CommonMoviesCount >= 2).OrderBy(x => x.FirstActorName).ThenBy(x => x.SecondActorName);

            return filteredOrderedActorPairs.ToList();
        }

        /// <summary>
        /// Query 4: Get director statistics for directors who have directed at least 2 movies, including the number of movies, average rating, total income, and highest income.
        /// </summary>
        /// <returns></returns>
        public List<DirectorStat> GetDirectorStat()
        {
            var directors = from d in directorRepository.GetAll()
                            where d.Movies.Count() >= 2 // LazyLoad!!
                            select new DirectorStat()
                            {
                                DirectorName = d.Name,
                                NumberOfMovies = d.Movies.Count(),
                                AverageRating = d.Movies.Select(x => x.Rating).Average(), // Not necessary the select
                                TotalIncome = d.Movies.Sum(x => x.Income),
                                HighestIncome = d.Movies.Max(x => x.Income)
                            };

            return directors.ToList(); // Note: not necessary to create local variable, but for debugging purpose, it is better to create a local variable
        }
    }
}
