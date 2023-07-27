using Application.Core;
using AutoMapper;
using MediatR;
using OrganizationService.Application.Core;
using OrganizationService.Application.Core.DTOs.Areas;
using OrganizationService.Application.Core.Interfaces;

namespace OrganizationService.Application.Features.Areas;

public class DetailQuery
{
    public class Query : IRequest<Response<AreaRDTO>>
    {
        public long Id { get; set; }
    }
    
    public class Handler : IRequestHandler<Query, Response<AreaRDTO>>
    {
        private readonly IArea _area;
        private readonly IMapper _mapper;

        public Handler(IArea area, IMapper mapper)
        {
            _area = area;
            _mapper = mapper;
        }
        
        public async Task<Response<AreaRDTO>> Handle(Query request, CancellationToken cancellationToken)
        {
            var area = await _area.GetByIdAsync(request.Id);
            if (area == null)
            {
                return Response<AreaRDTO>.Failure("Area not found");
            }
            return Response<AreaRDTO>.Success(_mapper.Map<AreaRDTO>(area));
        }
    }
}