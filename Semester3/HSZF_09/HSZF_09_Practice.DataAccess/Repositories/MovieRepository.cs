using HSZF_09_Practice.DataAccess.Contexts;
using HSZF_09_Practice.DataAccess.Interfaces;
using HSZF_09_Practice.Shared.Entities;

namespace HSZF_09_Practice.DataAccess.Repositories
{
    public class MovieRepository : RepositoryBase<int, Movie>, IMovieRepository
    {
        public MovieRepository(MovieDBContext context) : base(context)
        {
        }
    }
}
