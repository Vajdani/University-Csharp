using HSZF_04.Shared.Entities;

namespace HSZF_04.BusinessLogic.Interfaces
{
    public interface IPersonService
    {
        Director CreateDirector(Director director);
        void DeleteDirector(int id);
        List<Director> GetAllDirectors();
        Director? GetDirectorById(int id);
        Director UpdateDirector(Director director);
    }
}