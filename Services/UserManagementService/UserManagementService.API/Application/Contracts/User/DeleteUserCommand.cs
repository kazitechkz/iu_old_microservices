using AutoMapper;
using MediatR;
using UserManagementService.API.Application.DTO.ResponseDTO;
using UserManagementService.API.Application.DTO.UserDTO;
using UserManagementService.API.Application.Repositories;
using UserManagementService.API.Domain.Models;

namespace UserManagementService.API.Application.Contracts.User
{
    public class DeleteUserCommand : IRequest<ResponseRDTO<bool>>
    {
        public DeleteUserCommand(string userId)
        {
            UserId = userId;
        }
        public string UserId { get; set; }
    }

    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, ResponseRDTO<bool>>
    {
        private readonly IMapper mapper;
        private IUserRepository _userRepository;

        public DeleteUserCommandHandler(IMapper mapper, IUserRepository userRepository)
        {
            this.mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<ResponseRDTO<bool>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                ApplicationUser user = await _userRepository.GetUser(request.UserId);
                if (user == null)
                {
                    return new ResponseRDTO<bool>
                    {
                        StatusCode = 400,
                        Success = false,
                        Message = "User doesnt exist",
                    };
                }
                await _userRepository.DeleteUser(user);
                return new ResponseRDTO<bool>
                {
                    StatusCode = 200,
                    Success = true,
                    Data = true
                };
            }
            catch (Exception ex)
            {
                return new ResponseRDTO<bool>
                {
                    StatusCode = 500,
                    Success = false,
                    Message = ex.Message.ToString(),
                    Detail = ex.ToString(),
                };

            }
        }
    }
}
