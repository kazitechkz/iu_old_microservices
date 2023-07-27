using FluentValidation;
using OrganizationService.Application.Core.DTOs.Schools;

namespace OrganizationService.Application.Features.Schools;

public class Validator : AbstractValidator<SchoolCUD>
{
    public Validator()
    {
        RuleFor(x => x.TitleKk).NotEmpty();
        RuleFor(x => x.TitleRu).NotEmpty();
        RuleFor(x => x.TitleEn).NotEmpty();
        RuleFor(x => x.Code).NotEmpty();
        RuleFor(x => x.AreaId).NotEmpty();
        RuleFor(x => x.LegalFormId).NotEmpty();
    }
}