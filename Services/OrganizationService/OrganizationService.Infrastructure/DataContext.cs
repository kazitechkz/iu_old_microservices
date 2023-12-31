﻿using System.Reflection;
using Microsoft.EntityFrameworkCore;
using OrganizationService.Domain.ModelConfigurations;
using OrganizationService.Domain.Models;

namespace OrganizationService.Infrastructure;

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
    
    public DbSet<Area> Areas { get; set; }
    public DbSet<LegalForm> LegalForms { get; set; }
    public DbSet<School> Schools { get; set; }
}