using Microsoft.EntityFrameworkCore;
using OrganizationService.Domain.ModelConfigurations;
using OrganizationService.Domain.Models;

namespace OrganizationService.Infrastructure;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions options) : base(options) { }

    public virtual async Task<int> SaveChangesAsync(string username = "SYSTEM")
    {
        foreach (var entry in base.ChangeTracker.Entries<BaseModel>()
                     .Where(q => q.State == EntityState.Added || q.State == EntityState.Modified))
        {
            entry.Entity.UpdatedAt = DateTime.Now;
                
            if (entry.State == EntityState.Added)
            {
                entry.Entity.CreatedAt = DateTime.Now;
            }
        }
        var result = await base.SaveChangesAsync();
        return result;
    }
        
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new AreaConfiguration());
        modelBuilder.ApplyConfiguration(new LegalFormConfiguration());
        modelBuilder.ApplyConfiguration(new SchoolConfiguration());
    }
    
    public DbSet<Area> Areas { get; set; }
    public DbSet<LegalForm> LegalForms { get; set; }
    public DbSet<School> Schools { get; set; }
}