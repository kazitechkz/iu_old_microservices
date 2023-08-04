using AutoMapper;
using FileService.Application.Core.DTOs.UploadFiles;
using FileService.Application.Core.DTOs.UserFiles;
using FileService.Domain.Models;

namespace FileService.Application.Core
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            // CreateMap<UploadFileCUD, UploadFile>()
            //     .ForMember(x => x.Url, opt =>
            //         opt.MapFrom(src => new Helpers.FileService().SaveFile(src.Url)))
            //     .ReverseMap();
            // CreateMap<UploadFile, UploadFileRDTO>().ReverseMap();
            CreateMap<UploadFile, UploadFileCUD>().ReverseMap();
            CreateMap<UploadFile, UploadFileRDTO>().ReverseMap();
            CreateMap<UserFile, UserFileCUD>().ReverseMap();
            CreateMap<UserFile, UserFileRDTO>().ReverseMap();
        }
    }
}
