using AutoMapper;
using MediatR;
using OrganizationService.Application.Core;
using OrganizationService.Application.Core.DTOs.Areas;
using OrganizationService.Application.Core.Interfaces;

namespace OrganizationService.Application.Features.Areas;

public class ListQuery
{
    public class Query : IRequest<Response<List<AreaRDTO>>> { }
    
    public class Handler : IRequestHandler<Query, Response<List<AreaRDTO>>>
    {
        private readonly IArea _area;
        private readonly IMapper _mapper;

        public Handler(IArea area, IMapper mapper)
        {
            _area = area;
            _mapper = mapper;
        }
        
        public async Task<Response<List<AreaRDTO>>> Handle(Query request, CancellationToken cancellationToken)
        {
            var areas = await _area.ListAllAsync();
            return Response<List<AreaRDTO>>.Success(_mapper.Map<List<AreaRDTO>>(areas));
        }
    }
}