using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UserManagementService.API.Application.DTO.ResponseDTO;
using UserManagementService.API.Application.DTO.UserDTO;
using UserManagementService.API.Application.Repositories;
using UserManagementService.API.Domain.Models;
using UserManagementService.API.Infrastructure.Database;

namespace UserManagementService.API.Application.Contracts.User
{
    public class UpdateUserCommand : IRequest<ResponseRDTO<bool>>
    {
        public UpdateUserCommand(string userId, UserUDTO model)
        {
            UserId = userId;
            this.model = model;
        }

        public string UserId { get; set; }

        public UserUDTO model { get; set; }

    }

    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, ResponseRDTO<bool>>
    {
        private readonly AppDbContext _appDbContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper mapper;
        private IUserRepository _userRepository;

        public UpdateUserCommandHandler(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IMapper mapper, AppDbContext appDbContext, IUserRepository userRepository)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _appDbContext = appDbContext;
            this.mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<ResponseRDTO<bool>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                ApplicationUser user = await _userManager.FindByIdAsync(request.UserId);
                if (user == null){
                    return new ResponseRDTO<bool>
                    {
                        StatusCode = 400,
                        Data = false,
                        Success = false,
                        Message = "User doesnt exist",
                    };
                }
                if ((await _userManager.Users.Where(p => p.UserName == request.model.UserName && p.Id != request.UserId).FirstOrDefaultAsync()) != null ||
                   (await _userManager.Users.Where(p => p.UserName == request.model.UserName && p.Id != request.UserId).FirstOrDefaultAsync()) != null ||
                   (await _userManager.Users.Where(p => p.Email == request.model.Email && p.Id != request.UserId).FirstOrDefaultAsync()) != null ||
                   (await _userManager.Users.Where(p => p.PhoneNumber == request.model.PhoneNumber && p.Id != request.UserId).FirstOrDefaultAsync()) != null)
                {
                    return new ResponseRDTO<bool>
                    {
                        StatusCode = 400,
                        Data = false,
                        Message = "With this credentials user already exists",
                    };
                }
                if(!string.IsNullOrEmpty(request.model.RoleCode))
                {
                if ((await _roleManager.RoleExistsAsync(request.model.RoleCode)) == null)
                    {
                        return new ResponseRDTO<bool>
                        {
                            StatusCode = 400,
                            Data = false,
                            Success = false,
                            Message = "Role doesnt exist",
                        };
                    }
                }
                
                user = mapper.Map<UserUDTO,ApplicationUser>(request.model,user);
                await _userRepository.UpdateUserAsync(user, request.model.UserPassword,request.model.RoleCode);
                return new ResponseRDTO<bool>
                {
                    StatusCode = 201,
                    Data = true,
                    Success = true,
                    Message = "User Updated Successfully",
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
}
