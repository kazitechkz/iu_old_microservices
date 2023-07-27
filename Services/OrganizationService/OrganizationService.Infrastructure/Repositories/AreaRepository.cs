using OrganizationService.Application.Core.Interfaces;
using OrganizationService.Domain.Models;

namespace OrganizationService.Infrastructure.Repositories;

public class AreaRepository : Generic<Area>, IArea
{
    public AreaRepository(DataContext context) : base(context)
    {
    }
}