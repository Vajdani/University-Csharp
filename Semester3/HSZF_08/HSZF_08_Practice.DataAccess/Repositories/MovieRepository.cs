using HSZF_08_Practice.DataAccess.Contexts;
using HSZF_08_Practice.DataAccess.Interfaces;
using HSZF_08_Practice.Shared.Entities;

namespace HSZF_08_Practice.DataAccess.Repositories
{
    public class MovieRepository : RepositoryBase<int, Movie>, IMovieRepository
    {
        public MovieRepository(MovieDBContext context) : base(context)
        {
        }
    }
}
