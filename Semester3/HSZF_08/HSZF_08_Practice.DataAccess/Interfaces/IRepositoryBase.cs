namespace HSZF_08_Practice.DataAccess.Interfaces
{
    public interface IRepositoryBase<TKey, TEntity> where TEntity : class
    {
        TEntity Create(TEntity entity);
        void Delete(TKey id);
        IQueryable<TEntity> GetAll();
        TEntity? GetById(TKey id);
        TEntity Update(TEntity entity);
    }
}