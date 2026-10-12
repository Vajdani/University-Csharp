using HSZF_06_Practice.DataAccess.Contexts;
using HSZF_06_Practice.DataAccess.Interfaces;
using HSZF_06_Practice.Shared.Entities;

namespace HSZF_06_Practice.DataAccess.Repositories
{
    public class ActorRepository : RepositoryBase<int, Actor>, IActorRepository
    {
        public ActorRepository(MovieDBContext context) : base(context)
        {
        }

        public bool Exists(int id)
        {
            return context.Actors.Any(x => x.Id == id);
        }
    }
}
