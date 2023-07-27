using AutoMapper;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Application.Contracts.IRepositories;
using UserManagement.Application.DTO.ResponseDTO;
using UserManagement.Application.DTO.UserDTO;
using UserManagement.Application.Helpers;
using UserManagement.Domain.Models;

namespace UserManagement.Application.Features.User
{
    public class UserCreateCommand : IRequest<ResponseRDTO<UserRDTO>>
    {
        public UserCreateCommand(UserCDTO model)
        {
            this.model = model;
        }

        public UserCDTO model { get; set; }
    }


    public class UserCreateCommandHandler : IRequestHandler<UserCreateCommand, ResponseRDTO<UserRDTO>>
    {

        private readonly IUserRepository _userRepository;
        private readonly IUserRoleRepository _userRoleRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;

        public UserCreateCommandHandler(IUserRepository userRepository, IUserRoleRepository userRoleRepository, IRoleRepository roleRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _userRoleRepository = userRoleRepository;
            _roleRepository = roleRepository;
            _mapper = mapper;
        }

        public async Task<ResponseRDTO<UserRDTO>> Handle(UserCreateCommand request, CancellationToken cancellationToken)
        {
            try
            {
                string outputmessage = "";
                UserCDTO model = request.model;
                //First of all check if unique input doesnt exists;
                //Second give him a role, for it we will check: role,school,academicYear
                if ((await _userRepository.GetUserByEmail(model.Email)) != null)
                {
                    return new ResponseRDTO<UserRDTO>
                    {
                        StatusCode = 400,
                        Message = "This Email has already taken",
                        Success = false
                    };
                }
                if ((await _userRepository.GetUserByPhone(model.Phone)) != null)
                {
                    return new ResponseRDTO<UserRDTO>
                    {
                        StatusCode = 400,
                        Message = "This Phone has already taken",
                        Success = false
                    };
                }
                model.Password = SecurityHelper.EncryptPassword(model.Password);
                UserModel entity = _mapper.Map<UserModel>(model);
                entity = await _userRepository.AddAsync(entity);
                if (entity != null)
                {
                    outputmessage += "New User Has Been Created.";
                    //Now give him a role
                    if (model.Code != null)
                    {
                        RoleModel role = await _roleRepository.GetRoleByCode(model.Code);
                        if (role != null)
                        {
                            outputmessage = await this.CreateUserRoleAsync(entity, role, model);
                        }
                        else
                        {
                            outputmessage += "But giving role doesnt exist!";
                        }
                    }
                }
                return new ResponseRDTO<UserRDTO>
                {
                    StatusCode = 200,
                    Message = outputmessage,
                    Success = true,
                    Data = _mapper.Map<UserRDTO>(entity)
                };

            }
            catch (Exception ex)
            {
                return new ResponseRDTO<UserRDTO>
                {
                    StatusCode = 500,
                    Message = ex.Message,
                    Success = false,
                    Detail = ex.ToString(),
                };
            }
        }

        public async Task<string> CreateUserRoleAsync(UserModel user, RoleModel role, UserCDTO model)
        {
            try
            {
                UserRoleModel userRoleModel = new UserRoleModel
                {
                    UserId = user.Id,
                    RoleId = role.Id,
                    AcademicYearId = model.AcademicYearId,
                    SchoolId = model.SchoolId,
                    StartAt = model.StartAt ?? DateOnly.FromDateTime(DateTime.Now),
                    EndAt = model.EndAt ?? DateOnly.FromDateTime(DateTime.Now).AddYears(1),
                    Status = 1,
                };
                switch (role.Code)
                {
                    case DbConstants.GlobalAdminCode:
                        await _userRoleRepository.AddAsync(userRoleModel);
                        return $"We added user to role {role.TitleEn}";
                        break;
                    case DbConstants.GlobalModerCode:
                        await _userRoleRepository.AddAsync(userRoleModel);
                        return $"We added user to role {role.TitleEn}";
                        break;
                    default:
                        return $"We need to check school exist or not";
                        break;

                }
            }
            catch (Exception ex)
            {
                return $"Cant add user to role cause of {ex.Message}";

            };

        }
    
    
    
    }


    public class UserCreateValidator : AbstractValidator <UserCreateCommand>
    {
        public UserCreateValidator()
        {
            //Check     
            RuleFor<string>(p => p.model.Name)
                .NotNull().WithMessage("Field is Required")
                .NotEmpty().WithMessage("Field must be filled")
                .MaximumLength(255).WithMessage("Maximum Length is 255")
                .OverridePropertyName("Name");
            RuleFor<string>(p => p.model.Surname)
                .NotNull().WithMessage("Field is Required")
                .NotEmpty().WithMessage("Field must be filled")
                .MaximumLength(255).WithMessage("Maximum Length is 255")
                .OverridePropertyName("Surname");
            RuleFor<string>(p => p.model.MiddleName)
                .MaximumLength(255).WithMessage("Maximum Length is 255")
                .OverridePropertyName("MiddleName");
            RuleFor<int>(p => p.model.Status)
                .NotNull().WithMessage("Field is Required")
                .OverridePropertyName("Status");
            RuleFor<DateOnly>(p => p.model.BirthDate)
                .NotNull().WithMessage("Field is Required")
                .LessThan(DateOnly.FromDateTime(DateTime.Now)).WithMessage("Incorrect Data")
                .OverridePropertyName("BirthDate");
            RuleFor(r => r.model.Phone)
                 .NotNull().WithMessage("Field is Required")
                .NotEmpty().WithMessage("Phone is required")
                .Matches(@"^\+?77([0124567][0-8]\d{7})$").WithMessage("Phone must be valid number f.e - +77777777777")
                .OverridePropertyName("Phone");
            RuleFor(r => r.model.Email)
                .NotEmpty().WithMessage("Field is Required")
                .Matches(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$").WithMessage(x => "Field must be valid email")
                .OverridePropertyName("Email");
            RuleFor(r => r.model.Password)
                .NotEmpty().WithMessage("Password required")
                .Matches(@"^(?=.*[0-9]+.*)(?=.*[a-zA-Z]+.*)[0-9a-zA-Z@*_.]{6,}$").WithMessage("Длина пароля должна быть длинее 6 и включать буквы и цифры")
                .OverridePropertyName("Password");


        }


    }


}
