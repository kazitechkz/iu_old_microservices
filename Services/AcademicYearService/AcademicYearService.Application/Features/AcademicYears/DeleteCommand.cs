using AcademicYearService.Application.Core;
using AcademicYearService.Application.Core.Interfaces;
using MediatR;

namespace AcademicYearService.Application.Features.AcademicYears;

public class DeleteCommand
{
    public class Command : IRequest<Response<bool>>
    {
        public long Id { get; set; }
    }
    
    public class Handler : IRequestHandler<Command, Response<bool>>
    {
        private readonly IAcademicYear _academicYear;

        public Handler(IAcademicYear academicYear)
        {
            _academicYear = academicYear;
        }
        public async Task<Response<bool>> Handle(Command request, CancellationToken cancellationToken)
        {
            var academicYear = await _academicYear.GetByIdAsync(request.Id);
            if (academicYear == null)
            {
                return Response<bool>.Failure("Академический год не найден");
            }
            await _academicYear.DeleteAsync(academicYear);
            return Response<bool>.Success(true);
        }
    }
}