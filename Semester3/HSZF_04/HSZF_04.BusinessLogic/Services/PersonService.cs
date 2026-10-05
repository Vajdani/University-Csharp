using HSZF_04.BusinessLogic.Interfaces;
using HSZF_04.DataAccess.Interfaces;
using HSZF_04.Shared.Entities;

namespace HSZF_04.BusinessLogic.Services
{
    public class PersonService : IPersonService
    {
        IDirectorRepository directorRepository;
        IActorRepository actorRepository;

        public PersonService(IDirectorRepository directorRepository, IActorRepository actorRepository)
        {
            this.directorRepository = directorRepository;
            this.actorRepository = actorRepository;
        }

        #region Director CRUD
        public List<Director> GetAllDirectors()
        {
            //TODO: Check access.

            return directorRepository.GetAll().ToList();
        }

        public Director? GetDirectorById(int id)
        {
            //TODO: Check access.

            return directorRepository.GetById(id);
        }

        public Director CreateDirector(Director director)
        {
            //TODO: Check access.

            //TODO: Validate entity.
            if (String.IsNullOrWhiteSpace(director.Name))
            {
                throw new ArgumentNullException(nameof(director.Name));
            }

            var result = directorRepository.Create(director);
            director.Id = result.Id;

            return result;
        }

        public Director UpdateDirector(Director director)
        {
            //TODO: Check access.

            var existingDirector = directorRepository.GetById(director.Id);
            existingDirector.Name = director.Name;

            //TODO: Validate entity.
            if (String.IsNullOrWhiteSpace(existingDirector.Name))
            {
                throw new ArgumentNullException(nameof(existingDirector.Name));
            }

            return directorRepository.Update(existingDirector);
        }

        public void DeleteDirector(int id)
        {
            directorRepository.Delete(id);
        }
        #endregion

        #region Actor CRUD

        #endregion
    }
}
