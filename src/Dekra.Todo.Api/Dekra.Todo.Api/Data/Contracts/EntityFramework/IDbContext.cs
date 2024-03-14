using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore;

namespace Dekra.Todo.Api.Data.Contracts.EntityFramework
{
    public interface IDbContext
    {
        ChangeTracker ChangeTracker { get; }
        IModel Model { get; }
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
    }
}
