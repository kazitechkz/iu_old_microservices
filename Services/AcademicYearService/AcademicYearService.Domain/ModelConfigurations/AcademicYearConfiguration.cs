using AcademicYearService.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AcademicYearService.Domain.ModelConfigurations;

public class AcademicYearConfiguration : IEntityTypeConfiguration<AcademicYear>
{
    public void Configure(EntityTypeBuilder<AcademicYear> builder)
    {
        builder.Property(x => x.TitleKk).IsRequired().HasMaxLength(255);
        builder.Property(x => x.TitleRu).IsRequired().HasMaxLength(255);
        builder.Property(x => x.TitleEn).IsRequired().HasMaxLength(255);
        builder.Property(x => x.Code).IsRequired().HasMaxLength(255);
        builder.Property(x => x.StartAt).IsRequired();
        builder.Property(x => x.EndAt).IsRequired();
    }
}