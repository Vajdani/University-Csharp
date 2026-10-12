using HSZF_06_Practice.DataAccess.Contexts;
using HSZF_06_Practice.DataAccess.Interfaces;
using HSZF_06_Practice.Shared.Entities;
using Microsoft.EntityFrameworkCore;

namespace HSZF_06_Practice.DataAccess.Repositories
{
    public class RoleRepository : RepositoryBase<int, Role>, IRoleRepository
    {
        public RoleRepository(MovieDBContext context) : base(context)
        {
        }

        public IQueryable<Role> GetAll(bool withRelated)
        {
            if (withRelated) // In real application we want to granulate the includes with a configuration object. We don't have any time for that :(
            {
                return context.Roles
                    .Include(r => r.Actor)
                    .Include(r => r.Movie);
            }
            else
            {
                return GetAll();
            }
        }
    }
}
