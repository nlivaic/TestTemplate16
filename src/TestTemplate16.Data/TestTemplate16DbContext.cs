using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using TestTemplate16.Common.Base;
using TestTemplate16.Core.Entities;

namespace TestTemplate16.Data;

public class TestTemplate16DbContext : DbContext
{
    public TestTemplate16DbContext(DbContextOptions options)
        : base(options)
    {
    }

    public DbSet<Foo> Foos { get; set; }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        UpdateVersionableEntities();
        return base.SaveChangesAsync(cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.AddInboxStateEntity();
        modelBuilder.AddOutboxMessageEntity();
        modelBuilder.AddOutboxStateEntity();

        modelBuilder.Entity<Foo>(entity =>
        {
            entity.Property("RowVersion")
                .IsRowVersion();
        });
    }

    private void UpdateVersionableEntities()
    {
        if (!ChangeTracker.HasChanges())
        {
            return;
        }
        foreach (var item in ChangeTracker.Entries())
        {
            if (item.Entity is IVersionedEntity)
            {
                item.OriginalValues["RowVersion"] = item.CurrentValues["RowVersion"];
            }
        }
    }
}
