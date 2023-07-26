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
    public class UserModelConfiguration : IEntityTypeConfiguration<UserModel>
    {
        public void Configure(EntityTypeBuilder<UserModel> builder)
        {
            builder.Property<string>(p => p.Name).HasMaxLength(255).IsRequired();
            builder.Property<string>(p => p.Surname).HasMaxLength(255).IsRequired();
            builder.Property<string>(p => p.MiddleName).HasMaxLength(255);
            builder.HasIndex(p => p.Phone).IsUnique();
            builder.HasIndex(p => p.Email).IsUnique();
            builder.Property<string>(p=>p.Password).IsRequired();
            builder.Property<int>(p=>p.Status).IsRequired();
            //Foreign Key to Gender
            builder.HasOne(p => p.Gender).WithMany().HasForeignKey(e => e.GenderId).OnDelete(DeleteBehavior.SetNull);
        }
    }
}
