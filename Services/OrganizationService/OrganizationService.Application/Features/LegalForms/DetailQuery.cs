using Application.Core;
using AutoMapper;
using MediatR;
using OrganizationService.Application.Core;
using OrganizationService.Application.Core.DTOs.Areas;
using OrganizationService.Application.Core.DTOs.LegalForms;
using OrganizationService.Application.Core.Interfaces;

namespace OrganizationService.Application.Features.LegalForms;

public class DetailQuery
{
    public class Query : IRequest<Response<LegalFormRDTO>>
    {
        public long Id { get; set; }
    }
    
    public class Handler : IRequestHandler<Query, Response<LegalFormRDTO>>
    {
        private readonly ILegalForm _legalForm;
        private readonly IMapper _mapper;

        public Handler(ILegalForm legalForm, IMapper mapper)
        {
            _legalForm = legalForm;
            _mapper = mapper;
        }
        
        public async Task<Response<LegalFormRDTO>> Handle(Query request, CancellationToken cancellationToken)
        {
            var legalForm = await _legalForm.GetByIdAsync(request.Id);
            if (legalForm == null)
            {
                return Response<LegalFormRDTO>.Failure("LegalForm not found");
            }
            return Response<LegalFormRDTO>.Success(_mapper.Map<LegalFormRDTO>(legalForm));
        }
    }
}