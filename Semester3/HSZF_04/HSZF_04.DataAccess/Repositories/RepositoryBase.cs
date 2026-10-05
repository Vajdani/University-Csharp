using HSZF_04.DataAccess.Contexts;
using HSZF_04.DataAccess.Interfaces;

namespace HSZF_04.DataAccess.Repositories
{
    public abstract class RepositoryBase<TKey, TEntity> : IRepository<TKey, TEntity> where TEntity : class
    {
        protected MovieDBContext context;

        public RepositoryBase(MovieDBContext context)
        {
            this.context = context;
        }

        public IQueryable<TEntity> GetAll()
        {
            return context.Set<TEntity>();
        }

        public TEntity? GetById(TKey id)
        {
            return context.Set<TEntity>().Find(id);
        }

        public TEntity Create(TEntity entity)
        {
            var result = context.Set<TEntity>().Add(entity);
            context.SaveChanges();

            return result.Entity;
        }

        public TEntity Update(TEntity entity)
        {
            var result = context.Set<TEntity>().Update(entity);
            context.SaveChanges();

            return result.Entity;
        }

        public void Delete(TKey id)
        {
            var result = GetById(id);
            if (result is null)
            {
                throw new InvalidOperationException($"Entity not found by id {id}.");
            }

            context.Set<TEntity>().Remove(result);
            context.SaveChanges();
        }
    }
}
