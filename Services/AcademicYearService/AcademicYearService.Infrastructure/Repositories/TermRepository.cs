using AcademicYearService.Application.Core.Interfaces;
using AcademicYearService.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace AcademicYearService.Infrastructure.Repositories;

public class TermRepository : Generic<Term>, ITerm
{
    private readonly DataContext _context;

    public TermRepository(DataContext context) : base(context)
    {
        _context = context;
    }
    
    public async Task<Term> GetByCodeAsync(string code)
    {
        return await _context.Set<Term>().AsNoTracking().FirstOrDefaultAsync(p => p.Code == code);
    }
}