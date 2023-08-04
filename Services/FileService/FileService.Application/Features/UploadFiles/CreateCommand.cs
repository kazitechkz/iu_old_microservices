using AutoMapper;
using FileService.Application.Core;
using FileService.Application.Core.DTOs.UploadFiles;
using FileService.Application.Core.Interfaces;
using FileService.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace FileService.Application.Features.UploadFiles;

public class CreateCommand
{
    public class Command : IRequest<Response<UploadFileRDTO>>
    {
        public IFormFile File { get; set; }
        public string? SubDirectory { get; set; }
    }
    
    public class Handler : IRequestHandler<Command, Response<UploadFileRDTO>>
    {
        private readonly IUploadFile _uploadFile;
        private readonly IMapper _mapper;

        public Handler(IUploadFile uploadFile, IMapper mapper)
        {
            _uploadFile = uploadFile;
            _mapper = mapper;
        }
        
        public async Task<Response<UploadFileRDTO>> Handle(Command request, CancellationToken cancellationToken)
        {
            var savedFile = _uploadFile.SaveFile(request.File, subDirectory:request.SubDirectory);
            var uploadFile = _mapper.Map<UploadFile>(savedFile);
            await _uploadFile.AddAsync(uploadFile);
            return Response<UploadFileRDTO>.Success(_mapper.Map<UploadFileRDTO>(uploadFile));
        }
    }
}