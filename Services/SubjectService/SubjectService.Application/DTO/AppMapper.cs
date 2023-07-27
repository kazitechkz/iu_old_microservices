using AutoMapper;
using SubjectService.Application.DTO.BaseDTO;
using SubjectService.Application.DTO.GradeLevelDTO;
using SubjectService.Application.DTO.LanguageDTO;
using SubjectService.Application.DTO.SubjectDTO;
using SubjectService.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubjectService.Application.DTO
{
    public class AppMapper : Profile
    {
        public AppMapper()
        {
            CreateMap<BaseRDTO, BaseModel>().ReverseMap();

            CreateMap<GradeLevelRDTO, GradeLevelModel>().ReverseMap();
            CreateMap<GradeLevelCDTO, GradeLevelModel>().ReverseMap();
            CreateMap<GradeLevelUDTO, GradeLevelModel>().ReverseMap();

            CreateMap<LanguageRDTO, LanguageModel>().ReverseMap();
            CreateMap<LanguageCDTO, GradeLevelModel>().ReverseMap();
            CreateMap<LanguageUDTO, GradeLevelModel>().ReverseMap();

            CreateMap<SubjectRDTO, SubjectModel>().ReverseMap();
            CreateMap<SubjectCDTO, SubjectModel>().ReverseMap();
            CreateMap<SubjectUDTO, SubjectModel>().ReverseMap();

        }






    }
}
