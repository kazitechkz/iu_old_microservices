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
    public class UserUpdateCommand : IRequest<ResponseRDTO<UserRDTO>>
    {
        public UserUpdateCommand(UserUDTO model,long Id)
        {
            this.model = model;
            this.Id = Id;
        }
        public UserUDTO model { get; set; }
        public long Id { get; set; }

    }
    
    public class UserUpdateCommandHandler : IRequestHandler<UserUpdateCommand, ResponseRDTO<UserRDTO>>
    {

        private readonly IUserRepository _userRepository;
        private readonly IUserRoleRepository _userRoleRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;

        public UserUpdateCommandHandler(IUserRepository userRepository, IUserRoleRepository userRoleRepository, IRoleRepository roleRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _userRoleRepository = userRoleRepository;
            _roleRepository = roleRepository;
            _mapper = mapper;
        }

        public async Task<ResponseRDTO<UserRDTO>> Handle(UserUpdateCommand request, CancellationToken cancellationToken)
        {
            try
            {
               
                UserUDTO model = request.model;
                UserModel entity = await _userRepository.GetByIdAsync(request.Id);

                if(entity == null)
                {
                    return new ResponseRDTO<UserRDTO>
                    {
                        StatusCode = 404,
                        Message = "Not Found",
                        Success = false
                    };
                }
                UserModel userEmail = (await _userRepository.GetUserByEmail(model.Email));
                if (userEmail != null && userEmail.Id != request.Id)
                {
                    return new ResponseRDTO<UserRDTO>
                    {
                        StatusCode = 400,
                        Message = "This Email has already taken",
                        Success = false
                    };
                }
                UserModel userPhone = (await _userRepository.GetUserByPhone(model.Phone));
                if (userPhone != null && userPhone.Id != request.Id)
                {
                    return new ResponseRDTO<UserRDTO>
                    {
                        StatusCode = 400,
                        Message = "This Phone has already taken",
                        Success = false
                    };
                }
                if(model.Password != null && model.Password.Length > 0)
                {
                    model.Password = SecurityHelper.EncryptPassword(model.Password);
                }
                else
                {
                    model.Password = entity.Password;
                }
                entity = _mapper.Map<UserUDTO, UserModel>(model,entity);
                entity = await _userRepository.UpdateAsync(entity);
                
                return new ResponseRDTO<UserRDTO>
                {
                    StatusCode = 201,
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
    }

    public class UserUpdateValidator : AbstractValidator<UserUpdateCommand>
    {
        public UserUpdateValidator()
        {
            RuleFor(p => p.Id)
               .NotNull().WithMessage("Field is Required")
               .OverridePropertyName("Id");
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
                .When(r => !string.IsNullOrEmpty(r.model.Password))
                .Matches(@"^(?=.*[0-9]+.*)(?=.*[a-zA-Z]+.*)[0-9a-zA-Z@*_.]{6,}$").WithMessage("Длина пароля должна быть длинее 6 и включать буквы и цифры")
                .When(r => !string.IsNullOrEmpty(r.model.Password))
                .OverridePropertyName("Password");
        }


    }



}
