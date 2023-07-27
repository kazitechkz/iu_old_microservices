using AutoMapper;
using OrganizationService.Application.Core.DTOs.Areas;
using OrganizationService.Application.Core.DTOs.LegalForms;
using OrganizationService.Application.Core.DTOs.Schools;
using OrganizationService.Domain.Models;

namespace OrganizationService.Application.Core
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Area, AreaCUD>().ReverseMap();
            CreateMap<Area, AreaRDTO>().ReverseMap();
            CreateMap<LegalForm, LegalFormCUD>().ReverseMap();
            CreateMap<LegalForm, LegalFormRDTO>().ReverseMap();
            CreateMap<School, SchoolCUD>().ReverseMap();
            CreateMap<School, SchoolRDTO>().ReverseMap();
        }
    }
}
