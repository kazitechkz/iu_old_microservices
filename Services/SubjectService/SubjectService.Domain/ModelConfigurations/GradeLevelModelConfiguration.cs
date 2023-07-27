using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SubjectService.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubjectService.Domain.ModelConfigurations
{
    public class GradeLevelModelConfiguration : IEntityTypeConfiguration<GradeLevelModel>
    {
        public void Configure(EntityTypeBuilder<GradeLevelModel> builder)
        {
            builder.Property<string>(p => p.TitleRu).HasMaxLength(255).IsRequired();
            builder.Property<string>(p => p.TitleEn).HasMaxLength(255).IsRequired();
            builder.Property<string>(p => p.TitleKk).HasMaxLength(255).IsRequired();
            builder.HasIndex(p => p.Code).IsUnique();
            builder.Property<int>(p => p.Status).IsRequired();
        }
    }
}
