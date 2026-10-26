using HSZF_08_Practice.BusinessLogic.Interfaces;
using HSZF_08_Practice.BusinessLogic.Models;
using HSZF_08_Practice.DataAccess.Interfaces;
using HSZF_08_Practice.Shared.Entities;

namespace HSZF_08_Practice.BusinessLogic.Services
{
    public class RoleService : IRoleService
    {
        IRoleRepository roleRepository;
        IActorRepository actorRepository;
        IMovieRepository movieRepository;

        public RoleService(IRoleRepository roleRepository, IActorRepository actorRepository, IMovieRepository movieRepository) // Dependency Injection
        {
            this.roleRepository = roleRepository;
            this.actorRepository = actorRepository;
            this.movieRepository = movieRepository;
        }

        public List<Role> GetAll()
        {
            // TODO: check access

            return roleRepository.GetAll().ToList();
        }

        public Role? GetById(int id)
        {
            // TODO: check access

            return roleRepository.GetById(id);
        }

        public void Create(Role role)
        {
            // TODO: check permission

            // TODO: validate data
            if (string.IsNullOrWhiteSpace(role.Name)) // TODO: check length too
            {
                throw new ArgumentException("Role name cannot be empty."); // Or collect into a result object
            }

            // Note: this range is came from the Range attribute in the Role class, but we can also validate it here
            if (role.Priority < 0 || role.Priority > 10)
            {
                throw new ArgumentException("Role priority must be between 0 and 10.");
            }

            // Check actor and movie existence

            var result = roleRepository.Create(role);
            role.Id = result.Id; // Update the Role object with the generated ID

            // TODO: log the creation
        }

        public void UpdateRole(Role role)
        {
            // TODO: check permission

            // Get existing Role to ensure it exists before updating
            var existingRole = roleRepository.GetById(role.Id);

            if (existingRole == null)
            {
                throw new ArgumentException("Role not found.");
            }

            // Copy only the properties that can be updated
            existingRole.Name = role.Name;
            existingRole.Priority = role.Priority;
            existingRole.ActorId = role.ActorId; // We can restrict this if we don't want to allow changing the actor
            existingRole.MovieId = role.MovieId; // We can restrict this if we don't want to allow changing the movie

            // TODO: validate Role data
            if (string.IsNullOrWhiteSpace(existingRole.Name))
            {
                throw new ArgumentException("Role name cannot be empty."); // Or collect into a result object
            }

            // Note: this range is came from the Range attribute in the Role class, but we can also validate it here
            if (role.Priority < 0 || role.Priority > 10)
            {
                throw new ArgumentException("Role priority must be between 0 and 10.");
            }

            var result = roleRepository.Update(existingRole);

            // TODO: log the update
        }

        public void Delete(int id)
        {
            // TODO: check permission

            roleRepository.Delete(id); // now we can delete the role without checking if it exists first, as the repository will handle that and throw an exception if it doesn't exist. This is just an example, in a real application we might want to check if the role exists first and throw a more specific exception.
        }

        /// <summary>
        /// Query #2: Get actors with their roles in movies up to role priority 5
        /// </summary>
        /// <returns></returns>
        public List<RoleDisplay> GetRolesByActors()
        {
            var actorsWithRoles = (from actor in actorRepository.GetAll()
                                   join role in roleRepository.GetAll()
                                     on actor.Id equals role.ActorId
                                   join movie in movieRepository.GetAll()
                                     on role.MovieId equals movie.Id
                                   where role.Priority <= 5
                                   orderby actor.Name
                                   select new RoleDisplay()
                                   {
                                       ActorId = actor.Id,
                                       MovieId = movie.Id,
                                       ActorName = actor.Name,
                                       MovieTitle = movie.Title,
                                       RoleName = role.Name,
                                       Priority = role.Priority,
                                   }).ToList();

            return actorsWithRoles;
        }


        /// <summary>
        /// Query #2: Get actors with their roles in movies up to role priority 5
        /// </summary>
        /// <returns></returns>
        public List<RoleDisplay> GetRolesByActorsWithEagerLoad()
        {
            var actorsWithRoles2 = (from role in roleRepository.GetAll(true)
                                    where role.Priority <= 5
                                    orderby role.Actor.Name
                                    select new RoleDisplay()
                                    {
                                        ActorId = role.ActorId,
                                        MovieId = role.MovieId,
                                        ActorName = role.Actor.Name,
                                        MovieTitle = role.Movie.Title,
                                        RoleName = role.Name,
                                        Priority = role.Priority,
                                    }).ToList();

            return actorsWithRoles2;
        }
    }
}
