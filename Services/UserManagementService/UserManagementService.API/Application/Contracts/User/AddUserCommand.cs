using AutoMapper;
using FluentValidation;
using IdentityModel;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using UserManagementService.API.Application.DTO.ResponseDTO;
using UserManagementService.API.Application.DTO.UserDTO;
using UserManagementService.API.Application.Repositories;
using UserManagementService.API.Domain.Models;
using UserManagementService.API.Infrastructure.Database;

namespace UserManagementService.API.Application.Contracts.User
{
    public class AddUserCommand : IRequest<ResponseRDTO<bool>>
    {
        public AddUserCommand(UserCDTO model)
        {
            this.model = model;
        }

        public UserCDTO model { get; set; }
    }

    public class AddUserCommandHandler : IRequestHandler<AddUserCommand, ResponseRDTO<bool>>
    {
        private readonly AppDbContext _appDbContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper mapper;
        private IUserRepository _userRepository;

        public AddUserCommandHandler(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IMapper mapper, AppDbContext appDbContext, IUserRepository userRepository)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _appDbContext = appDbContext;
            this.mapper = mapper;
            _userRepository = userRepository;
        }



        public async Task<ResponseRDTO<bool>> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if((await _userManager.FindByEmailAsync(request.model.Email)) != null || 
                   (await _userManager.FindByNameAsync(request.model.UserName)) != null ||
                   (await _userManager.Users.Where(p=>p.PhoneNumber == request.model.PhoneNumber).FirstOrDefaultAsync()) != null) 
                {
                    return new ResponseRDTO<bool>
                    {
                        StatusCode = 400,
                        Data = false,
                        Success = false,
                        Message = "With this credentials user already exists",
                    };
                }

                if((await _roleManager.RoleExistsAsync(request.model.RoleCode)) == null)
                {
                    return new ResponseRDTO<bool>
                    {
                        StatusCode = 400,
                        Data = false,
                        Success = false,
                        Message = "Role doesnt exist",
                    };
                }
                ApplicationUser user = mapper.Map<ApplicationUser>(request.model);
                await _userRepository.AddUserAsync(user,request.model.UserPassword,request.model.RoleCode);
                return new ResponseRDTO<bool>
                {
                    StatusCode = 200,
                    Data = true,
                    Success = true,
                    Message = "User Created Successfully",
                };
            }
            catch (Exception ex)
            {
                return new ResponseRDTO<bool>
                {
                    StatusCode = 500,
                    Data = false,
                    Success = false,
                    Message = ex.Message.ToString(),
                    Detail = ex.ToString(),
                };
            }

        }
    }

    public class AddUserCommandValidator : AbstractValidator<AddUserCommand>
    {
        public AddUserCommandValidator()
        {
            RuleFor(r => r.model.IIN)
             .Must(u => u.Length == 12).WithMessage("IIN MUST CONSIST OF 12 DIGITS")
                 .Matches(@"^[0-9]*$").WithMessage("IIN MUST BE 12 DIGITS")
                 .OverridePropertyName("IIN");

            RuleFor(r => r.model.UserPassword)
                .NotEmpty().WithMessage("Password required")
                .Matches(@"^(?=.*[0-9]+.*)(?=.*[a-zA-Z]+.*)[0-9a-zA-Z@*_.]{6,}$")
                .WithMessage("Password should consist of digits, word and special characters")
                .OverridePropertyName("Password");
            RuleFor(r => r.model.Name)
                .NotEmpty().WithMessage("This field is required")
                .Matches(@"[а-яА-ЯёЁәӘіІңҢғҒүҮұҰқҚөӨһҺA-Za-z]+").WithMessage("Name must consist of only words")
                .OverridePropertyName("Name");
            RuleFor(r => r.model.Surname)
                .NotEmpty().WithMessage("This field is required")
                .Matches(@"[а-яА-ЯёЁәӘіІңҢғҒүҮұҰқҚөӨһҺA-Za-z]+").WithMessage("Name must consist of only words")
                .OverridePropertyName("Surname");
            RuleFor(r => r.model.PhoneNumber)
                .NotEmpty().WithMessage("This field is required")
                .Matches(@"^\+?77([0124567][0-8]\d{7})$").WithMessage("Phone must starts with + 77---------")
                .OverridePropertyName("PhoneNumber");
            RuleFor(r => r.model.BirthDate)
                .Must(d=>d < DateOnly.FromDateTime(DateTime.Now)).WithMessage("Incorrect Date")
                .OverridePropertyName("BirthDate");
            RuleFor(r => r.model.Email)
                .NotEmpty().WithMessage("This field is required")
                .Matches(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$").WithMessage("Please write unique Email type")
                .OverridePropertyName("Email");
        }
    }
}
