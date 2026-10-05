using HSZF_04.DataAccess.Contexts;
using HSZF_04.Shared.Entities;

namespace HSZF_04.DataAccess.Repositories
{
    public class MovieRepository : RepositoryBase<int, Movie>
    {
        public MovieRepository(MovieDBContext context) : base(context) { }
    }
}
