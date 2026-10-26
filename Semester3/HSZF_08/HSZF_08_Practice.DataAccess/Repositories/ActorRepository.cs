using HSZF_08_Practice.DataAccess.Contexts;
using HSZF_08_Practice.DataAccess.Interfaces;
using HSZF_08_Practice.Shared.Entities;

namespace HSZF_08_Practice.DataAccess.Repositories
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
