using System.Data;
using FluentValidation;
using OrganizationService.Application.Core.DTOs.Areas;
using OrganizationService.Application.Core.DTOs.LegalForms;

namespace OrganizationService.Application.Features.LegalForms;

public class Validator : AbstractValidator<LegalFormCUD>
{
    public Validator()
    {
        RuleFor(x => x.TitleKk).NotEmpty();
        RuleFor(x => x.TitleRu).NotEmpty();
        RuleFor(x => x.TitleEn).NotEmpty();
        RuleFor(x => x.Code).NotEmpty();
    }
}