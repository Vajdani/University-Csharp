using HSZF_08_Practice.DataAccess.Contexts;
using HSZF_08_Practice.DataAccess.Interfaces;
using HSZF_08_Practice.Shared.Entities;

namespace HSZF_08_Practice.DataAccess.Repositories
{
    public class DirectorRepository : RepositoryBase<int, Director>, IDirectorRepository
    {
        public DirectorRepository(MovieDBContext context) : base(context)
        {
        }

        public bool Exists(int id)
        {
            return context.Directors.Any(x => x.Id == id);
        }
    }
}
