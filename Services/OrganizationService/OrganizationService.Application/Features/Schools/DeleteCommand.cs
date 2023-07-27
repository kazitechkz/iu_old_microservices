using MediatR;
using OrganizationService.Application.Core;
using OrganizationService.Application.Core.Interfaces;

namespace OrganizationService.Application.Features.Schools;

public class DeleteCommand
{
    public class Command : IRequest<Response<bool>>
    {
        public long Id { get; set; }
    }
    
    public class Handler : IRequestHandler<Command, Response<bool>>
    {
        private readonly ISchool _school;

        public Handler(ISchool school)
        {
            _school = school;
        }
        public async Task<Response<bool>> Handle(Command request, CancellationToken cancellationToken)
        {
            var school = await _school.GetByIdAsync(request.Id);
            if (school == null)
            {
                return Response<bool>.Failure("School not found");
            }
            await _school.DeleteAsync(school);
            return Response<bool>.Success(true);
        }
    }
}