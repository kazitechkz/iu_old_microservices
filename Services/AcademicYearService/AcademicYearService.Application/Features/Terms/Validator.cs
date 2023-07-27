using AcademicYearService.Application.Core.DTOs.AcademicYears;
using AcademicYearService.Application.Core.DTOs.Terms;
using FluentValidation;

namespace AcademicYearService.Application.Features.Terms;

public class Validator : AbstractValidator<TermCUD>
{
    public Validator()
    {
        RuleFor(x => x.TitleKk).NotEmpty();
        RuleFor(x => x.TitleRu).NotEmpty();
        RuleFor(x => x.TitleEn).NotEmpty();
        RuleFor(x => x.Code).NotEmpty();
        RuleFor(x => x.StartAt).NotEmpty();
        RuleFor(x => x.EndAt).NotEmpty();
        RuleFor(x => x.AcademicYearId).NotEmpty();
    }
}