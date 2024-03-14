namespace Dekra.Todo.Api.Data.Contracts.EntityFramework
{
    public interface IRepositoryFactory
    {
        IRepository<TEntity> GetRepository<TEntity>() where TEntity : class;
    }
}
