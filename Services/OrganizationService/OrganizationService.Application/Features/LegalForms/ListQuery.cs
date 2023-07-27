using AutoMapper;
using MediatR;
using OrganizationService.Application.Core;
using OrganizationService.Application.Core.DTOs.LegalForms;
using OrganizationService.Application.Core.Interfaces;

namespace OrganizationService.Application.Features.LegalForms;

public class ListQuery
{
    public class Query : IRequest<Response<List<LegalFormRDTO>>> { }
    
    public class Handler : IRequestHandler<Query, Response<List<LegalFormRDTO>>>
    {
        private readonly ILegalForm _legalForm;
        private readonly IMapper _mapper;

        public Handler(ILegalForm legalForm, IMapper mapper)
        {
            _legalForm = legalForm;
            _mapper = mapper;
        }
        
        public async Task<Response<List<LegalFormRDTO>>> Handle(Query request, CancellationToken cancellationToken)
        {
            var legalForms = await _legalForm.ListAllAsync();
            return Response<List<LegalFormRDTO>>.Success(_mapper.Map<List<LegalFormRDTO>>(legalForms));
        }
    }
}