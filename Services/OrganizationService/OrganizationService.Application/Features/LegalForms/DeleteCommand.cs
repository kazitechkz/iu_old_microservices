using Application.Core;
using MediatR;
using OrganizationService.Application.Core;
using OrganizationService.Application.Core.Interfaces;

namespace OrganizationService.Application.Features.LegalForms;

public class DeleteCommand
{
    public class Command : IRequest<Response<bool>>
    {
        public long Id { get; set; }
    }
    
    public class Handler : IRequestHandler<Command, Response<bool>>
    {
        private readonly ILegalForm _legalForm;

        public Handler(ILegalForm legalForm)
        {
            _legalForm = legalForm;
        }
        
        public async Task<Response<bool>> Handle(Command request, CancellationToken cancellationToken)
        {
            var legalForm = await _legalForm.GetByIdAsync(request.Id);
            if (legalForm == null) { return Response<bool>.Failure("LegalForm not found"); }
            await _legalForm.DeleteAsync(legalForm);
            return Response<bool>.Success(true);
        }
    }
}