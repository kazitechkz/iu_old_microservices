using Microsoft.EntityFrameworkCore;
using OrganizationService.Application.Core.Interfaces;
using OrganizationService.Domain.Models;

namespace OrganizationService.Infrastructure.Repositories;

public class SchoolRepository : Generic<School>, ISchool
{
    private readonly DataContext _context;

    public SchoolRepository(DataContext context) : base(context)
    {
        _context = context;
    }
    
    public async Task<School> GetByCodeAsync(string code)
    {
        return await _context.Set<School>().AsNoTracking().FirstOrDefaultAsync(p => p.Code == code);
    }
}