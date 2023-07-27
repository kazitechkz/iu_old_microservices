using AcademicYearService.Application.Core.Interfaces;
using AcademicYearService.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace AcademicYearService.Infrastructure.Repositories;

public class AcademicYearRepository : Generic<AcademicYear>, IAcademicYear
{
    private readonly DataContext _context;

    public AcademicYearRepository(DataContext context) : base(context)
    {
        _context = context;
    }

    public async Task<AcademicYear> GetByCodeAsync(string code)
    {
        return await _context.Set<AcademicYear>().AsNoTracking().FirstOrDefaultAsync(p => p.Code == code);
    }
}