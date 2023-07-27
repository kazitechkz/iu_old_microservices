using OrganizationService.Application.Core.Interfaces;
using OrganizationService.Domain.Models;

namespace OrganizationService.Infrastructure.Repositories;

public class LegalFormRepository : Generic<LegalForm>, ILegalForm
{
    public LegalFormRepository(DataContext context) : base(context)
    {
    }
}