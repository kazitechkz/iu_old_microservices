using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrganizationService.Domain.Models;

namespace OrganizationService.Domain.ModelConfigurations;

public class SchoolConfiguration : IEntityTypeConfiguration<School>
{
    public void Configure(EntityTypeBuilder<School> builder)
    {
        builder.HasIndex(x => x.Code).IsUnique();
        builder.Property(x => x.TitleKk).IsRequired().HasMaxLength(255);
        builder.Property(x => x.TitleRu).IsRequired().HasMaxLength(255);
        builder.Property(x => x.TitleEn).IsRequired().HasMaxLength(255);
        builder.Property(x => x.Code).IsRequired().HasMaxLength(255);
        builder.Property(x => x.Status).IsRequired();
        builder.Property(x => x.AreaId).IsRequired();
        builder.Property(x => x.LegalFormId).IsRequired();
    }
}