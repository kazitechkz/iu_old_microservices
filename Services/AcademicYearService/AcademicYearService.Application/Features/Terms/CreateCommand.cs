using AcademicYearService.Application.Core;
using AcademicYearService.Application.Core.DTOs.AcademicYears;
using AcademicYearService.Application.Core.DTOs.Terms;
using AcademicYearService.Application.Core.Interfaces;
using AcademicYearService.Domain.Models;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace AcademicYearService.Application.Features.Terms;

public class CreateCommand
{
    public class Command : IRequest<Response<TermRDTO>>
    {
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
        private readonly ITerm _term;
        private readonly IMapper _mapper;
        private readonly IAcademicYear _academicYear;

        public Handler(ITerm term, IMapper mapper, IAcademicYear academicYear)
        {
            _term = term;
            _mapper = mapper;
            _academicYear = academicYear;
        }
        public async Task<Response<TermRDTO>> Handle(Command request, CancellationToken cancellationToken)
        {
            var academicYear = await _academicYear.GetByIdAsync(request.TermCud.AcademicYearId);
            if (academicYear == null)
            {
                return Response<TermRDTO>.Failure("Выбранный академический год не существует!");
            }

            var code = await _term.GetByCodeAsync(request.TermCud.Code);
            if (code != null)
            {
                return Response<TermRDTO>.Failure("Такая четверть уже есть в базе!");
            }
            var term = _mapper.Map<Term>(request.TermCud);
            await _term.AddAsync(term);
            return Response<TermRDTO>.Success(_mapper.Map<TermRDTO>(term));
        }
    }
}