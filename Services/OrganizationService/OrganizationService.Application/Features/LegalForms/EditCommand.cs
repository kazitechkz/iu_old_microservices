using Application.Core;
using AutoMapper;
using FluentValidation;
using MediatR;
using OrganizationService.Application.Core;
using OrganizationService.Application.Core.DTOs.LegalForms;
using OrganizationService.Application.Core.Interfaces;
using OrganizationService.Domain.Models;

namespace OrganizationService.Application.Features.LegalForms;

public class EditCommand
{
    public class Command : IRequest<Response<LegalFormRDTO>>
    {
        public long Id { get; set; }
        public LegalFormCUD legalFormCud { get; set; }
    }
    
    public class CommandValidator : AbstractValidator<Command>
    {
        public CommandValidator()
        {
            RuleFor(x => x.legalFormCud).SetValidator(new Validator());
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
            var legalForm = await _legalForm.GetByIdAsync(request.Id);
            if (legalForm == null) { return Response<LegalFormRDTO>.Failure("LegalForm not found"); }
            _mapper.Map(request.legalFormCud, legalForm);
            await _legalForm.UpdateAsync(legalForm);
            return Response<LegalFormRDTO>.Success(_mapper.Map<LegalFormRDTO>(legalForm));
        }
    }
}