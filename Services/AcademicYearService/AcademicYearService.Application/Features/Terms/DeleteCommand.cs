using AcademicYearService.Application.Core;
using AcademicYearService.Application.Core.Interfaces;
using MediatR;

namespace AcademicYearService.Application.Features.Terms;

public class DeleteCommand
{
    public class Command : IRequest<Response<bool>>
    {
        public long Id { get; set; }
    }
    
    public class Handler : IRequestHandler<Command, Response<bool>>
    {
        private readonly ITerm _term;

        public Handler(ITerm term)
        {
            _term = term;
        }
        public async Task<Response<bool>> Handle(Command request, CancellationToken cancellationToken)
        {
            var term = await _term.GetByIdAsync(request.Id);
            if (term == null)
            {
                return Response<bool>.Failure("Четверть не найдена");
            }
            await _term.DeleteAsync(term);
            return Response<bool>.Success(true);
        }
    }
}