namespace Gecko.Bcon.DataAccess.Repositories
{
    /// <summary>
    /// Repository Interface 
    /// </summary>
    public interface IRepository<TEntity> where TEntity : class
    {
        bool Save(TEntity entity);
        bool Delete(TEntity entity);
    }
}