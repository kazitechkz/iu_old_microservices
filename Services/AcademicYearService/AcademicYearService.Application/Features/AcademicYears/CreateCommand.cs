using AcademicYearService.Application.Core;
using AcademicYearService.Application.Core.DTOs.AcademicYears;
using AcademicYearService.Application.Core.Interfaces;
using AcademicYearService.Domain.Models;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace AcademicYearService.Application.Features.AcademicYears;

public class CreateCommand
{
    public class Command : IRequest<Response<AcademicYearRDTO>>
    {
        public AcademicYearCUD AcademicYearCud;
    }
    
    public class CommandValidator : AbstractValidator<Command>
    {
        public CommandValidator()
        {
            RuleFor(x => x.AcademicYearCud).SetValidator(new Validator());
        }
    }
    
    public class Handler : IRequestHandler<Command, Response<AcademicYearRDTO>>
    {
        private readonly IAcademicYear _academicYear;
        private readonly IMapper _mapper;

        public Handler(IAcademicYear academicYear, IMapper mapper)
        {
            _academicYear = academicYear;
            _mapper = mapper;
        }
        public async Task<Response<AcademicYearRDTO>> Handle(Command request, CancellationToken cancellationToken)
        {
            var code = await _academicYear.GetByCodeAsync(request.AcademicYearCud.Code);
            if (code != null)
            {
                return Response<AcademicYearRDTO>.Failure("Такой академический год уже есть в базе!");
            }
            var academicYear = _mapper.Map<AcademicYear>(request.AcademicYearCud);
            await _academicYear.AddAsync(academicYear);
            return Response<AcademicYearRDTO>.Success(_mapper.Map<AcademicYearRDTO>(academicYear));
        }
    }
}