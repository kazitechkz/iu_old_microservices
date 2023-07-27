using AcademicYearService.Application.Core;
using AcademicYearService.Application.Core.DTOs.AcademicYears;
using AcademicYearService.Application.Core.Interfaces;
using AcademicYearService.Application.Core.Specification;
using AcademicYearService.Domain.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;

namespace AcademicYearService.Application.Features.AcademicYears;

public class ListQuery
{
    public class Query : IRequest<Response<PagedList<AcademicYearRDTO>>>
    {
        public ISpecification<AcademicYear> specification;
        public AcademicYearParameters parameters;
    }

    public class Handler : IRequestHandler<Query, Response<PagedList<AcademicYearRDTO>>>
    {
        private readonly IAcademicYear _academicYear;
        private readonly IMapper _mapper;

        public Handler(IAcademicYear academicYear, IMapper mapper)
        {
            _academicYear = academicYear;
            _mapper = mapper;
        }
        public async Task<Response<PagedList<AcademicYearRDTO>>> Handle(Query request, CancellationToken cancellationToken)
        {
            var academicYears = _academicYear.GetQueryable(request.specification);
            var query = academicYears.ProjectTo<AcademicYearRDTO>(_mapper.ConfigurationProvider);
            return Response<PagedList<AcademicYearRDTO>>.Success(await PagedList<AcademicYearRDTO>.CreateAsync(query, request.parameters.PageNumber, request.parameters.PageSize));
        }
    }
}