using HSZF_04.DataAccess.Contexts;
using HSZF_04.Shared.Entities;

namespace HSZF_04.DataAccess.Repositories
{
    public class ActorRepository : RepositoryBase<int, Actor>
    {
        public ActorRepository(MovieDBContext context) : base(context) { }
    }
}
