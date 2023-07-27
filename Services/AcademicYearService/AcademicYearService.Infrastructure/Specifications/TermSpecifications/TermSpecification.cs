using AcademicYearService.Domain.Models;

namespace AcademicYearService.Infrastructure.Specifications.TermSpecifications;

public class TermSpecification : BaseSpecification<Term>
{
    public TermSpecification()
    {
        AddInclude("AcademicYear");
    }
}