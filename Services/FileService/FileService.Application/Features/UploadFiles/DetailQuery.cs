using AutoMapper;
using FileService.Application.Core;
using FileService.Application.Core.DTOs.UploadFiles;
using FileService.Application.Core.Interfaces;
using FluentValidation;
using MediatR;

namespace FileService.Application.Features.UploadFiles;

public class DetailQuery
{
    public class Query : IRequest<Response<UploadFileRDTO>>
    {
        public long Id { get; set; }
    }
    
    public class CommandValidator : AbstractValidator<Query>
    {
        public CommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
        }
    }
    
    public class Handler : IRequestHandler<Query, Response<UploadFileRDTO>>
    {
        private readonly IUploadFile _uploadFile;
        private readonly IMapper _mapper;

        public Handler(IUploadFile uploadFile, IMapper mapper)
        {
            _uploadFile = uploadFile;
            _mapper = mapper;
        }
        
        public async Task<Response<UploadFileRDTO>> Handle(Query request, CancellationToken cancellationToken)
        {
            var file = await _uploadFile.GetByIdAsync(request.Id);
            if (file == null || file.IsDeleted)
            {
                return Response<UploadFileRDTO>.Failure("File not found");
            }
            return Response<UploadFileRDTO>.Success(_mapper.Map<UploadFileRDTO>(file));
        }
    }
}