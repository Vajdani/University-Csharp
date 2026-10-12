using HSZF_06_Practice.Shared.Entities;

namespace HSZF_06_Practice.DataAccess.Interfaces
{
    public interface IRoleRepository : IRepositoryBase<int, Role>
    {
        // Place any additional methods specific to Role repository here, if needed.
        IQueryable<Role> GetAll(bool withRelated);
    }
}
