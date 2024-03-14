using Dekra.Todo.Api.Data.Contracts.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace Dekra.Todo.Api.Data.Entities
{
    public class DekraDbContext : Microsoft.EntityFrameworkCore.DbContext, IDbContext
    {
        public DekraDbContext(DbContextOptions<DekraDbContext> options) : base(options) { }

        public virtual DbSet<WorkItem> WorkItems { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<WorkItem>(entity =>
            {
                entity.HasKey(e => e.Id);
            });
        }
    }
}
