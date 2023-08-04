using AutoMapper;
using FileService.Application.Core;
using FileService.Application.Core.DTOs.UploadFiles;
using FileService.Application.Core.DTOs.UserFiles;
using FileService.Application.Core.Interfaces;
using MediatR;

namespace FileService.Application.Features.UserFiles;

public class DetailQuery
{
    public class Query : IRequest<Response<UserFileRDTO>>
    {
        public string IIN { get; set; }
        public long UserId { get; set; }
    }
    
    public class Handler : IRequestHandler<Query, Response<UserFileRDTO>>
    {
        private readonly IUserFile _userFile;
        private readonly IMapper _mapper;

        public Handler(IUserFile userFile, IMapper mapper)
        {
            _userFile = userFile;
            _mapper = mapper;
        }
        
        public async Task<Response<UserFileRDTO>> Handle(Query request, CancellationToken cancellationToken)
        {
            return Response<UserFileRDTO>.Failure("");
        }
    }
}