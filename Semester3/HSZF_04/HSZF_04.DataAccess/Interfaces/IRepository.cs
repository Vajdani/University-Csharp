namespace HSZF_04.DataAccess.Interfaces
{
    public interface IRepository<TKey, TEntity> where TEntity : class
    {
        TEntity Create(TEntity entity);
        void Delete(TKey id);
        IQueryable<TEntity> GetAll();
        TEntity? GetById(TKey id);
        TEntity Update(TEntity entity);
    }
}