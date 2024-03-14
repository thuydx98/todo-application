using Microsoft.EntityFrameworkCore;

namespace Dekra.Todo.Api.Data.Contracts.EntityFramework
{
    public interface IUnitOfWork : IRepositoryFactory, IDisposable
    {
        int Commit();
        Task<int> CommitAsync();
    }

    public interface IUnitOfWork<TContext> : IUnitOfWork where TContext : DbContext
    {
        TContext Context { get; }
    }
}
