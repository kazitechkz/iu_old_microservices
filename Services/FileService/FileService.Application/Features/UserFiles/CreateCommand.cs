using AutoMapper;
using FileService.Application.Core;
using FileService.Application.Core.DTOs.UploadFiles;
using FileService.Application.Core.DTOs.UserFiles;
using FileService.Application.Core.Interfaces;
using FileService.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace FileService.Application.Features.UserFiles;

public class CreateCommand
{
    public class Command : IRequest<Response<UserFileRDTO>>
    {
        public FromFormDTO FromFormDto { get; set; }
    }
    
    public class Handler : IRequestHandler<Command, Response<UserFileRDTO>>
    {
        private readonly IUploadFile _uploadFile;
        private readonly IMapper _mapper;
        private readonly IUserFile _userFile;

        public Handler(IUploadFile uploadFile, IMapper mapper, IUserFile userFile)
        {
            _uploadFile = uploadFile;
            _mapper = mapper;
            _userFile = userFile;
        }
        
        public async Task<Response<UserFileRDTO>> Handle(Command request, CancellationToken cancellationToken)
        {
            var subDirectory = $"users/{request.FromFormDto.IIN}/{request.FromFormDto.UserId}";
            var saveFile = _uploadFile.SaveFile(request.FromFormDto.File, subDirectory);
            var uploadedFile = await _uploadFile.AddAsync(_mapper.Map<UploadFile>(saveFile));
            var userFileCud = new UserFileCUD
            {
                Title = request.FromFormDto.Title,
                IIN = request.FromFormDto.IIN,
                UserId = request.FromFormDto.UserId,
                UploadFileId = uploadedFile.Id
            };
            var userFile = await _userFile.AddAsync(_mapper.Map<UserFile>(userFileCud));
            return Response<UserFileRDTO>.Success(_mapper.Map<UserFileRDTO>(userFile));
        }
    }
}