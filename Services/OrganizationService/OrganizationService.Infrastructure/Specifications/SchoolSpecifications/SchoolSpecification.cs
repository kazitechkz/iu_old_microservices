using OrganizationService.Domain.Models;

namespace OrganizationService.Infrastructure.Specifications.SchoolSpecifications;

public class SchoolSpecification : BaseSpecification<School>
{
    public SchoolSpecification()
    {
        AddInclude("Area");
        AddInclude("LegalForm");
    }
}