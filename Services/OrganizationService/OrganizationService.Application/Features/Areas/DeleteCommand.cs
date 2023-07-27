using Application.Core;
using MediatR;
using OrganizationService.Application.Core;
using OrganizationService.Application.Core.Interfaces;

namespace OrganizationService.Application.Features.Areas;

public class DeleteCommand
{
    public class Command : IRequest<Response<bool>>
    {
        public long Id { get; set; }
    }
    
    public class Handler : IRequestHandler<Command, Response<bool>>
    {
        private readonly IArea _area;

        public Handler(IArea area)
        {
            _area = area;
        }
        
        public async Task<Response<bool>> Handle(Command request, CancellationToken cancellationToken)
        {
            var area = await _area.GetByIdAsync(request.Id);
            if (area == null) { return Response<bool>.Failure("Area not found"); }
            await _area.DeleteAsync(area);
            return Response<bool>.Success(true);
        }
    }
}