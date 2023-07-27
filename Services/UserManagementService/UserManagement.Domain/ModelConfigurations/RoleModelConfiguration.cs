using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Domain.Models;

namespace UserManagement.Domain.ModelConfigurations
{
    public class RoleModelConfiguration : IEntityTypeConfiguration<RoleModel>
    {
        public void Configure(EntityTypeBuilder<RoleModel> builder)
        {
            builder.Property<string>(p => p.TitleRu).HasMaxLength(255).IsRequired();
            builder.Property<string>(p => p.TitleEn).HasMaxLength(255).IsRequired();
            builder.Property<string>(p => p.TitleKk).HasMaxLength(255).IsRequired();
            builder.HasIndex(p => p.Code).IsUnique();
            builder.Property<int>(p => p.Status).IsRequired();
        }
    }
}
