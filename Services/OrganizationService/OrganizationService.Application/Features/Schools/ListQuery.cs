using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using OrganizationService.Application.Core;
using OrganizationService.Application.Core.DTOs.Schools;
using OrganizationService.Application.Core.Interfaces;
using OrganizationService.Application.Core.Specification;
using OrganizationService.Domain.Models;

namespace OrganizationService.Application.Features.Schools;

public class ListQuery
{
    public class Query : IRequest<Response<PagedList<SchoolRDTO>>>
    {
        public ISpecification<School> specification;
        public SchoolParameters parameters;
    }
    
    public class Handler : IRequestHandler<Query, Response<PagedList<SchoolRDTO>>>
    {
        private readonly ISchool _school;
        private readonly IMapper _mapper;

        public Handler(ISchool school, IMapper mapper)
        {
            _school = school;
            _mapper = mapper;
        }
        
        public async Task<Response<PagedList<SchoolRDTO>>> Handle(Query request, CancellationToken cancellationToken)
        {
            var schools = _school.GetQueryable(request.specification);
            var query = schools.ProjectTo<SchoolRDTO>(_mapper.ConfigurationProvider);
            return Response<PagedList<SchoolRDTO>>.Success(await PagedList<SchoolRDTO>.CreateAsync(query, request.parameters.PageNumber, request.parameters.PageSize));
        }
    }
}