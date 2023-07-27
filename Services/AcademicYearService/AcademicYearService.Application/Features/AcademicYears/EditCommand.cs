using AcademicYearService.Application.Core;
using AcademicYearService.Application.Core.DTOs.AcademicYears;
using AcademicYearService.Application.Core.Interfaces;
using AcademicYearService.Domain.Models;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace AcademicYearService.Application.Features.AcademicYears;

public class EditCommand
{
    public class Command : IRequest<Response<AcademicYearRDTO>>
    {
        public long Id { get; set; }
        public AcademicYearCUD AcademicYearCud;
    }
    
    public class CommandValidator : AbstractValidator<CreateCommand.Command>
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
            var academicYear = await _academicYear.GetByIdAsync(request.Id);
            if (academicYear == null)
            {
                return Response<AcademicYearRDTO>.Failure("Академический год не найден");
            }

            if (academicYear.Code != request.AcademicYearCud.Code)
            {
                var code = await _academicYear.GetByCodeAsync(request.AcademicYearCud.Code);
                if (code != null)
                {
                    return Response<AcademicYearRDTO>.Failure("Такой академический год уже есть в базе!");
                }
            }
            _mapper.Map(request.AcademicYearCud, academicYear);
            await _academicYear.UpdateAsync(academicYear);
            return Response<AcademicYearRDTO>.Success(_mapper.Map<AcademicYearRDTO>(academicYear));
        }
    }
}