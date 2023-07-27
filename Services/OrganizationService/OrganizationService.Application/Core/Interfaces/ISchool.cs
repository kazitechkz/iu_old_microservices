using OrganizationService.Domain.Models;

namespace OrganizationService.Application.Core.Interfaces;

public interface ISchool : IGeneric<School>
{
    Task<School> GetByCodeAsync(string code);
}