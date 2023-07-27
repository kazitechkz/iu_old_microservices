using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SubjectService.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubjectService.Domain.ModelConfigurations
{
    public class LanguageModelConfiguration : IEntityTypeConfiguration<LanguageModel>
    {
        public void Configure(EntityTypeBuilder<LanguageModel> builder)
        {
            builder.Property<string>(p => p.TitleRu).HasMaxLength(255).IsRequired();
            builder.Property<string>(p => p.TitleEn).HasMaxLength(255).IsRequired();
            builder.Property<string>(p => p.TitleKk).HasMaxLength(255).IsRequired();
            builder.HasIndex(p => p.Code).IsUnique();
            builder.Property<int>(p => p.Status).IsRequired();
        }
    }
}
