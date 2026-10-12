using HSZF_06_Practice.DataAccess.Contexts;
using HSZF_06_Practice.DataAccess.Interfaces;
using HSZF_06_Practice.Shared.Entities;

namespace HSZF_06_Practice.DataAccess.Repositories
{
    public class MovieRepository : RepositoryBase<int, Movie>, IMovieRepository
    {
        public MovieRepository(MovieDBContext context) : base(context)
        {
        }
    }
}
