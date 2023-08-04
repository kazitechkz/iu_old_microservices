using System.Reflection;
using FileService.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace FileService.Infrastructure;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions options) : base(options) { }

    public override int SaveChanges()
    {
        UpdateSoftDeleteStatuses();
        return base.SaveChanges();
    }
    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
    {
        UpdateSoftDeleteStatuses();
        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }

    private void UpdateSoftDeleteStatuses()
    {
        foreach (var entry in ChangeTracker.Entries())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.CurrentValues["CreatedAt"] = DateTime.UtcNow;
                    break;
                case EntityState.Modified:
                    entry.CurrentValues["UpdatedAt"] = DateTime.UtcNow;
                    break;
                case EntityState.Deleted:
                    entry.State = EntityState.Modified;
                    entry.CurrentValues["IsDeleted"] = true;
                    entry.CurrentValues["DeletedAt"] = DateTime.UtcNow;
                    break;
            }
        }
    }
        
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
    
    public DbSet<UploadFile> UploadFiles { get; set; }
    public DbSet<UserFile> UserFiles { get; set; }
}