using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Domain.Models;

namespace UserManagement.Domain.ModelConfigurations
{
    public class UserRoleModelConfiguration : IEntityTypeConfiguration<UserRoleModel>
    {
        public void Configure(EntityTypeBuilder<UserRoleModel> builder)
        {
            builder.Property<long>(p => p.UserId).IsRequired();
            builder.Property<long>(p => p.RoleId).IsRequired();
            builder.Property<DateOnly>(p => p.StartAt).IsRequired();
            builder.Property<DateOnly>(p => p.EndAt).IsRequired();
            builder.Property<int>(p => p.Status).IsRequired();
        }
    }
}
