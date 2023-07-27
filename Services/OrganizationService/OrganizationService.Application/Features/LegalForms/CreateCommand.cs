using Application.Core;
using AutoMapper;
using FluentValidation;
using MediatR;
using OrganizationService.Application.Core;
using OrganizationService.Application.Core.DTOs.Areas;
using OrganizationService.Application.Core.DTOs.LegalForms;
using OrganizationService.Application.Core.Interfaces;
using OrganizationService.Domain.Models;

namespace OrganizationService.Application.Features.LegalForms;

public class CreateCommand
{
    public class Command : IRequest<Response<LegalFormRDTO>>
    {
        public LegalFormCUD LegalFormCud { get; set; }
    }
    
    public class CommandValidator : AbstractValidator<Command>
    {
        public CommandValidator()
        {
            RuleFor(x => x.LegalFormCud).SetValidator(new Validator());
        }
    }
    
    public class Handler : IRequestHandler<Command, Response<LegalFormRDTO>>
    {
        private readonly ILegalForm _legalForm;
        private readonly IMapper _mapper;

        public Handler(ILegalForm legalForm, IMapper mapper)
        {
            _legalForm = legalForm;
            _mapper = mapper;
        }
        
        public async Task<Response<LegalFormRDTO>> Handle(Command request, CancellationToken cancellationToken)
        {
            var legalForm = _mapper.Map<LegalForm>(request.LegalFormCud);
            await _legalForm.AddAsync(legalForm);
            return Response<LegalFormRDTO>.Success(_mapper.Map<LegalFormRDTO>(legalForm));
        }
    }
}