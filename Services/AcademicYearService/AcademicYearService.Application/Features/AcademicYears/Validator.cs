using AcademicYearService.Application.Core.DTOs.AcademicYears;
using FluentValidation;

namespace AcademicYearService.Application.Features.AcademicYears;

public class Validator : AbstractValidator<AcademicYearCUD>
{
    public Validator()
    {
        RuleFor(x => x.TitleKk).NotEmpty();
        RuleFor(x => x.TitleRu).NotEmpty();
        RuleFor(x => x.TitleEn).NotEmpty();
        RuleFor(x => x.Code).NotEmpty();
        RuleFor(x => x.StartAt).NotEmpty();
        RuleFor(x => x.EndAt).NotEmpty();
    }
}