using AcademicYearService.Application.Core;
using AcademicYearService.Application.Core.DTOs.AcademicYears;
using AcademicYearService.Application.Core.DTOs.Terms;
using AcademicYearService.Application.Core.Interfaces;
using AcademicYearService.Domain.Models;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace AcademicYearService.Application.Features.Terms;

public class EditCommand
{
    public class Command : IRequest<Response<TermRDTO>>
    {
        public long Id { get; set; }
        public TermCUD TermCud;
    }
    
    public class CommandValidator : AbstractValidator<Command>
    {
        public CommandValidator()
        {
            RuleFor(x => x.TermCud).SetValidator(new Validator());
        }
    }
    
    public class Handler : IRequestHandler<Command, Response<TermRDTO>>
    {
        private readonly IAcademicYear _academicYear;
        private readonly IMapper _mapper;
        private readonly ITerm _term;

        public Handler(IAcademicYear academicYear, IMapper mapper, ITerm term)
        {
            _academicYear = academicYear;
            _mapper = mapper;
            _term = term;
        }

        public async Task<Response<TermRDTO>> Handle(Command request, CancellationToken cancellationToken)
        {
            var term = await _term.GetByIdAsync(request.Id);
            if (term == null)
            {
                return Response<TermRDTO>.Failure("Четверть не найдена!");
            }
            var academicYear = await _academicYear.GetByIdAsync(request.Id);
            if (academicYear == null)
            {
                return Response<TermRDTO>.Failure("Академический год не найден");
            }

            if (term.Code != request.TermCud.Code)
            {
                var code = _term.GetByCodeAsync(request.TermCud.Code);
                if (code != null)
                {
                    return Response<TermRDTO>.Failure("Такая четверть уже есть в базе!");
                }
            }
            _mapper.Map(request.TermCud, term);
            await _term.UpdateAsync(term);
            return Response<TermRDTO>.Success(_mapper.Map<TermRDTO>(term));
        }
    }
}