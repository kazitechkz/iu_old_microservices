using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Domain.Models;

namespace UserManagement.Infrastructure.Interceptors
{
    public class ChangeBaseInterceptor : SaveChangesInterceptor
    {

        public override InterceptionResult<int> SavingChanges(
        DbContextEventData eventData,
        InterceptionResult<int> result)
        {
            if (eventData.Context is null) return result;

            foreach (var entry in eventData.Context.ChangeTracker.Entries())
            {
                if(entry.Entity is BaseModel)
                {
                    if(entry is { State:EntityState.Added })
                    {
                        ((BaseModel)entry.Entity).CreatedAt = DateTime.Now;
                    }
                    if (entry is { State: EntityState.Modified })
                    {
                        ((BaseModel)entry.Entity).UpdatedAt = DateTime.Now;
                    }
                    if (entry is  { State: EntityState.Deleted, Entity: ISoftDelete delete })
                    {
                        entry.State = EntityState.Modified;
                        delete.IsDeleted = true;
                        delete.DeletedAt = DateTime.Now;
                    };
                }
                
            }
            return result;
        }

    }
}
