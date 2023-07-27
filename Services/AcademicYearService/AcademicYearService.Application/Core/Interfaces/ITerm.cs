using AcademicYearService.Domain.Models;

namespace AcademicYearService.Application.Core.Interfaces;

public interface ITerm : IGeneric<Term>
{
    Task<Term> GetByCodeAsync(string code);
}