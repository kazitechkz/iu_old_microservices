using AutoMapper;
using MediatR;
using OrganizationService.Application.Core;
using OrganizationService.Application.Core.DTOs.Schools;
using OrganizationService.Application.Core.Interfaces;

namespace OrganizationService.Application.Features.Schools;

public class DetailQuery
{
    public class Query : IRequest<Response<SchoolRDTO>>
    {
        public long Id { get; set; }
    }
    
    public class Handler : IRequestHandler<Query, Response<SchoolRDTO>>
    {
        private readonly ISchool _school;
        private readonly IMapper _mapper;

        public Handler(ISchool school, IMapper mapper)
        {
            _school = school;
            _mapper = mapper;
        }
        public async Task<Response<SchoolRDTO>> Handle(Query request, CancellationToken cancellationToken)
        {
            var school = await _school.GetByIdAsync(request.Id);
            if (school == null)
            {
                return Response<SchoolRDTO>.Failure("School not found");
            }
            return Response<SchoolRDTO>.Success(_mapper.Map<SchoolRDTO>(school));
        }
    }
}