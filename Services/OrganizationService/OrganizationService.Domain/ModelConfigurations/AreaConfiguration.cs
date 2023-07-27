using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrganizationService.Domain.Models;

namespace OrganizationService.Domain.ModelConfigurations;

public class AreaConfiguration : IEntityTypeConfiguration<Area>
{
    public void Configure(EntityTypeBuilder<Area> builder)
    {
        builder.Property(x => x.TitleKk).IsRequired().HasMaxLength(255);
        builder.Property(x => x.TitleRu).IsRequired().HasMaxLength(255);
        builder.Property(x => x.TitleEn).IsRequired().HasMaxLength(255);
    }
}