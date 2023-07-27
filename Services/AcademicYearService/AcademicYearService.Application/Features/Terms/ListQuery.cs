using AcademicYearService.Application.Core;
using AcademicYearService.Application.Core.DTOs.AcademicYears;
using AcademicYearService.Application.Core.DTOs.Terms;
using AcademicYearService.Application.Core.Interfaces;
using AcademicYearService.Application.Core.Specification;
using AcademicYearService.Domain.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;

namespace AcademicYearService.Application.Features.Terms;

public class ListQuery
{
    public class Query : IRequest<Response<PagedList<TermRDTO>>>
    {
        public ISpecification<Term> specification;
        public TermParameters parameters;
    }

    public class Handler : IRequestHandler<Query, Response<PagedList<TermRDTO>>>
    {
        private readonly ITerm _term;
        private readonly IMapper _mapper;

        public Handler(ITerm term, IMapper mapper)
        {
            _term = term;
            _mapper = mapper;
        }
        public async Task<Response<PagedList<TermRDTO>>> Handle(Query request, CancellationToken cancellationToken)
        {
            var terms = _term.GetQueryable(request.specification);
            var query = terms.ProjectTo<TermRDTO>(_mapper.ConfigurationProvider);
            return Response<PagedList<TermRDTO>>.Success(await PagedList<TermRDTO>.CreateAsync(query, request.parameters.PageNumber, request.parameters.PageSize));
        }
    }
}