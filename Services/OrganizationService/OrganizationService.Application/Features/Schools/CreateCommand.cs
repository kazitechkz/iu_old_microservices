using AutoMapper;
using FluentValidation;
using MediatR;
using OrganizationService.Application.Core;
using OrganizationService.Application.Core.DTOs.Schools;
using OrganizationService.Application.Core.Interfaces;
using OrganizationService.Domain.Models;

namespace OrganizationService.Application.Features.Schools;

public class CreateCommand
{
    public class Command : IRequest<Response<SchoolRDTO>>
    {
        public SchoolCUD schoolCud { get; set; }
    }
    
    public class CommandValidator : AbstractValidator<Command>
    {
        public CommandValidator()
        {
            RuleFor(x => x.schoolCud).SetValidator(new Validator());
        }
    }
    
    public class Handler : IRequestHandler<Command, Response<SchoolRDTO>>
    {
        private readonly ISchool _school;
        private readonly IMapper _mapper;
        private readonly IArea _area;
        private readonly ILegalForm _legalForm;

        public Handler(ISchool school, IMapper mapper, IArea area, ILegalForm legalForm)
        {
            _school = school;
            _mapper = mapper;
            _area = area;
            _legalForm = legalForm;
        }
        public async Task<Response<SchoolRDTO>> Handle(Command request, CancellationToken cancellationToken)
        {
            var area = await _area.GetByIdAsync(request.schoolCud.AreaId);
            if (area == null)
            {
                return Response<SchoolRDTO>.Failure("Area not found");
            }
            var legalForm = await _legalForm.GetByIdAsync(request.schoolCud.LegalFormId);
            if (legalForm == null)
            {
                return Response<SchoolRDTO>.Failure("LegalForm not found");
            }
            var code = await _school.GetByCodeAsync(request.schoolCud.Code);
            if (code != null)
            {
                return Response<SchoolRDTO>.Failure("Такая школа уже есть в базе!");
            }
            var school = _mapper.Map<School>(request.schoolCud);
            await _school.AddAsync(school);
            return Response<SchoolRDTO>.Success(_mapper.Map<SchoolRDTO>(school));
        }
    }
}