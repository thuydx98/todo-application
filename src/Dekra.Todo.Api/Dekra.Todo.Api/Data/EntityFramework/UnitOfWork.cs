using Dekra.Todo.Api.Data.Contracts.Entities;
using Dekra.Todo.Api.Data.Contracts.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Dekra.Todo.Api.Data.EntityFramework
{
    public class UnitOfWork<TContext> : IUnitOfWork<TContext> where TContext : DbContext
    {
        private const string NameIdentifierClaim = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier";
        private readonly IHttpContextAccessor? httpContextAccessor;
        private Dictionary<Type, object> repositories;

        public TContext Context { get; }


        public UnitOfWork(TContext context, IHttpContextAccessor? httpContextAccessor = null)
        {
            Context = context;
            this.httpContextAccessor = httpContextAccessor;
        }

        public int Commit()
        {
            TrackChanges();
            return Context.SaveChanges();
        }

        public async Task<int> CommitAsync()
        {
            //TrackChanges();
            return await Context.SaveChangesAsync();
        }

        public void Dispose()
        {
            Context?.Dispose();
        }

        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : class
        {
            repositories ??= new Dictionary<Type, object>();

            if (repositories.TryGetValue(typeof(TEntity), out object? repository))
            {
                return (IRepository<TEntity>)repository;
            }

            repository = new Repository<TEntity>(Context);

            repositories.Add(typeof(TEntity), repository);

            return (IRepository<TEntity>)repository;
        }

        private void TrackChanges()
        {
            var validationErrors = Context.ChangeTracker
                .Entries<IValidatableObject>()
                .SelectMany(e => e.Entity.Validate(null))
                .Where(r => r != ValidationResult.Success)
                .ToArray();

            if (validationErrors.Any())
            {
                var exceptionMessage = string.Join(
                    separator: Environment.NewLine,
                    values: validationErrors.Select(error => $"Properties {error.MemberNames} Error: {error.ErrorMessage}"));

                throw new Exception(exceptionMessage);
            }

            foreach (var entry in Context.ChangeTracker.Entries().Where(e => e.State == EntityState.Added))
            {
                if (entry?.Entity is not ICreatedEntity createdEntity) continue;

                createdEntity.CreatedBy = httpContextAccessor?.HttpContext?.User.FindFirst(NameIdentifierClaim)?.Value ?? "Anonymous";
                createdEntity.CreatedAt = DateTime.UtcNow;
            }

            foreach (var entry in Context.ChangeTracker.Entries().Where(e => e.State == EntityState.Modified))
            {
                if (entry?.Entity is not IUpdatedEntity updatedEntity) continue;

                updatedEntity.UpdatedBy = httpContextAccessor?.HttpContext?.User.FindFirst(NameIdentifierClaim)?.Value ?? "Anonymous";
                updatedEntity.UpdatedAt = DateTime.UtcNow;
            }
        }
    }
}
