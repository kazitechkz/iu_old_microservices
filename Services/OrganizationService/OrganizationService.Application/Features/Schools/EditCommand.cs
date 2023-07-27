using AutoMapper;
using FluentValidation;
using MediatR;
using OrganizationService.Application.Core;
using OrganizationService.Application.Core.DTOs.Schools;
using OrganizationService.Application.Core.Interfaces;
using OrganizationService.Domain.Models;

namespace OrganizationService.Application.Features.Schools;

public class EditCommand
{
    public class Command : IRequest<Response<SchoolRDTO>>
    {
        public long Id { get; set; }
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
            var school = await _school.GetByIdAsync(request.Id);
            if (school == null)
            {
                return Response<SchoolRDTO>.Failure("School not found");
            }
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

            if (school.Code != request.schoolCud.Code)
            {
                var code = await _school.GetByCodeAsync(request.schoolCud.Code);
                if (code != null)
                {
                    return Response<SchoolRDTO>.Failure("Такой код школы уже есть в базе!");
                }
            }
            
            _mapper.Map(request.schoolCud, school);
            await _school.UpdateAsync(school);
            return Response<SchoolRDTO>.Success(_mapper.Map<SchoolRDTO>(school));
        }
    }
}