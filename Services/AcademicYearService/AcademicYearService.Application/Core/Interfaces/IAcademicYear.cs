using AcademicYearService.Domain.Models;

namespace AcademicYearService.Application.Core.Interfaces;

public interface IAcademicYear : IGeneric<AcademicYear>
{
    Task<AcademicYear> GetByCodeAsync(string code);
}