using AcademicYearService.Application.Core.DTOs.AcademicYears;
using AcademicYearService.Application.Core.DTOs.Terms;
using AcademicYearService.Domain.Models;
using AutoMapper;

namespace AcademicYearService.Application.Core
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<AcademicYear, AcademicYearCUD>().ReverseMap();
            CreateMap<AcademicYear, AcademicYearRDTO>().ReverseMap();
            CreateMap<Term, TermCUD>().ReverseMap();
            CreateMap<Term, TermRDTO>().ReverseMap();
        }
    }
}
