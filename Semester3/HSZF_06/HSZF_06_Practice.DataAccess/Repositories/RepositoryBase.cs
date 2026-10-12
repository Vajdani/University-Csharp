using HSZF_06_Practice.DataAccess.Contexts;
using HSZF_06_Practice.DataAccess.Interfaces;

namespace HSZF_06_Practice.DataAccess.Repositories
{
    public abstract class RepositoryBase<TKey, TEntity> : IRepositoryBase<TKey, TEntity> where TEntity : class
    {
        protected MovieDBContext context;

        protected RepositoryBase(MovieDBContext context)
        {
            this.context = context;
        }

        public IQueryable<TEntity> GetAll()
        {
            return context.Set<TEntity>();
        }

        public virtual TEntity? GetById(TKey id)
        {
            return context.Set<TEntity>().Find(id);
        }

        public TEntity Create(TEntity entity)
        {
            var result = context.Add(entity);
            context.SaveChanges();

            return result.Entity;
        }

        public TEntity Update(TEntity entity)
        {
            var result = context.Update(entity);
            context.SaveChanges();

            return result.Entity;
        }

        public void Delete(TKey id)
        {
            var entity = GetById(id);

            if (entity == null)
            {
                throw new ArgumentException($"Entity with id {id} not found."); // Or return silently, depending on your requirements
            }

            context.Remove(entity);
            context.SaveChanges();
        }
    }
}
