using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Application.DTO.BaseDTO;
using UserManagement.Application.DTO.GenderDTO;
using UserManagement.Application.DTO.RoleDTO;
using UserManagement.Application.DTO.UserDTO;
using UserManagement.Application.DTO.UserRoleDTO;
using UserManagement.Domain.Models;

namespace UserManagement.Application.DTO
{
    public class AppMapper : Profile
    {
        public AppMapper()
        {
            //Base
            CreateMap<BaseRDTO,BaseModel>().ReverseMap();
            //Role
            CreateMap<RoleRDTO,RoleModel>().ReverseMap();
            CreateMap<RoleCDTO,RoleModel>().ReverseMap();
            CreateMap<RoleUDTO, RoleModel>().ReverseMap();
            //Gender
            CreateMap<GenderRDTO, GenderModel>().ReverseMap();
            CreateMap<GenderCDTO, GenderModel>().ReverseMap();
            CreateMap<GenderUDTO, GenderModel>().ReverseMap();
            //User 
            CreateMap<UserRDTO, UserModel>().ReverseMap();
            CreateMap<UserCDTO, UserModel>().ReverseMap();
            CreateMap<UserUDTO, UserModel>().ReverseMap();
            //User Role
            CreateMap<UserRoleRDTO, UserRoleModel>().ReverseMap();
            CreateMap<UserRoleUDTO, UserRoleModel>().ReverseMap();
            CreateMap<UserRoleCDTO, UserRoleModel>().ReverseMap();
        }
    }
}
