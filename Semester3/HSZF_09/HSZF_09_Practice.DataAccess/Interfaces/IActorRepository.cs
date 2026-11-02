using HSZF_09_Practice.Shared.Entities;

namespace HSZF_09_Practice.DataAccess.Interfaces
{
    public interface IActorRepository : IRepositoryBase<int, Actor>
    {
        // Place any additional methods specific to Actor repository here, if needed.
        bool Exists(int id);
    }
}
