using FileService.Application.Core;
using FileService.Application.Core.Interfaces;
using FluentValidation;
using MediatR;

namespace FileService.Application.Features.UploadFiles;

public class DeleteCommand
{
    public class Command : IRequest<Response<bool>>
    {
        public long Id { get; set; }
    }
    
    public class CommandValidator : AbstractValidator<Command>
    {
        public CommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
        }
    }
    
    public class Handler : IRequestHandler<Command, Response<bool>>
    {
        private readonly IUploadFile _uploadFile;

        public Handler(IUploadFile uploadFile)
        {
            _uploadFile = uploadFile;
        }
        public async Task<Response<bool>> Handle(Command request, CancellationToken cancellationToken)
        {
            var file = await _uploadFile.GetByIdAsync(request.Id);
            if (file == null)
            {
                return Response<bool>.Failure("File not found");
            }
            await _uploadFile.DeleteAsync(file);
            return Response<bool>.Success(true);
        }
    }
}