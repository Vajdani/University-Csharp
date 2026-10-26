using HSZF_08_Practice.Shared.Entities;

namespace HSZF_08_Practice.DataAccess.Interfaces
{
    public interface IDirectorRepository : IRepositoryBase<int, Director>
    {
        // Place any additional methods specific to Director repository here, if needed.
        bool Exists(int id);
    }
}
