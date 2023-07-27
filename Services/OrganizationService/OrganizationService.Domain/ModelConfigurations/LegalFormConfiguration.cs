using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrganizationService.Domain.Models;

namespace OrganizationService.Domain.ModelConfigurations;

public class LegalFormConfiguration : IEntityTypeConfiguration<LegalForm>
{
    public void Configure(EntityTypeBuilder<LegalForm> builder)
    {
        builder.HasIndex(x => x.Code).IsUnique();
        builder.Property(x => x.TitleKk).IsRequired().HasMaxLength(255);
        builder.Property(x => x.TitleRu).IsRequired().HasMaxLength(255);
        builder.Property(x => x.TitleEn).IsRequired().HasMaxLength(255);
        builder.Property(x => x.Code).IsRequired().HasMaxLength(255);
    }
}