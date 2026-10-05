using HSZF_04.DataAccess.Contexts;
using HSZF_04.Shared.Entities;

namespace HSZF_04.DataAccess.Repositories
{
    public class DirectorRepository : RepositoryBase<int, Director>
    {
        public DirectorRepository(MovieDBContext context) : base(context) { }
    }
}
