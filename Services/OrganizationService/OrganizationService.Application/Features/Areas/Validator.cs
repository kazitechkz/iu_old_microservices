using System.Data;
using FluentValidation;
using OrganizationService.Application.Core.DTOs.Areas;

namespace OrganizationService.Application.Features.Areas;

public class Validator : AbstractValidator<AreaCUD>
{
    public Validator()
    {
        RuleFor(x => x.TitleKk).NotEmpty();
        RuleFor(x => x.TitleRu).NotEmpty();
        RuleFor(x => x.TitleEn).NotEmpty();
    }
}