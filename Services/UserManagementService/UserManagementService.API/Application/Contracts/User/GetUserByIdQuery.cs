using AutoMapper;
using MediatR;
using UserManagementService.API.Application.DTO.ResponseDTO;
using UserManagementService.API.Application.DTO.UserDTO;
using UserManagementService.API.Application.Repositories;
using UserManagementService.API.Domain.Models;

namespace UserManagementService.API.Application.Contracts.User
{
    public class GetUserByIdQuery : IRequest<ResponseRDTO<UserRDTO>>
    {
        public GetUserByIdQuery(string userId)
        {
            UserId = userId;
        }
        public string UserId { get; set; } 

    }

    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, ResponseRDTO<UserRDTO>>
    {
        private readonly IMapper mapper;
        private IUserRepository _userRepository;

        public GetUserByIdQueryHandler(IMapper mapper, IUserRepository userRepository)
        {
            this.mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<ResponseRDTO<UserRDTO>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                ApplicationUser user = await _userRepository.GetUser(request.UserId);
                if(user == null)
                {
                    return new ResponseRDTO<UserRDTO>
                    {
                        StatusCode = 400,
                        Success = false,
                        Message = "User doesnt exist",
                    };
                }
                return new ResponseRDTO<UserRDTO>
                {
                    StatusCode = 200,
                    Success = true,
                    Data = mapper.Map<UserRDTO>(user)
                };



            }
            catch(Exception ex)
            {
                return new ResponseRDTO<UserRDTO>
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
