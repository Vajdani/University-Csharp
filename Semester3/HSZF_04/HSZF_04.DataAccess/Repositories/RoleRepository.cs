using HSZF_04.DataAccess.Contexts;
using HSZF_04.Shared.Entities;

namespace HSZF_04.DataAccess.Repositories
{
    public class RoleRepository : RepositoryBase<int, Role>
    {
        public RoleRepository(MovieDBContext context) : base(context) { }
    }
}
