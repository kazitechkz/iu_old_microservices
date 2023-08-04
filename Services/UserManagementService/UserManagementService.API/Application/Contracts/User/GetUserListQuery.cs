using AutoMapper;
using MediatR;
using UserManagementService.API.Application.DTO.ResponseDTO;
using UserManagementService.API.Application.DTO.UserDTO;
using UserManagementService.API.Application.Parameters;
using UserManagementService.API.Application.Repositories;
using UserManagementService.API.Domain.Models;

namespace UserManagementService.API.Application.Contracts.User
{
    public class GetUserListQuery : IRequest<ResponseRDTO<Pagination<UserRDTO>>>
    {
        public GetUserListQuery(UserParameter parameters)
        {
            this.parameters = parameters;
        }

        public UserParameter parameters { get; set; }

    }

    public class GetUserListQueryHandler : IRequestHandler<GetUserListQuery, ResponseRDTO<Pagination<UserRDTO>>>
    {
        private readonly IMapper mapper;
        private IUserRepository _userRepository;

        public GetUserListQueryHandler(IMapper mapper, IUserRepository userRepository)
        {
            this.mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<ResponseRDTO<Pagination<UserRDTO>>> Handle(GetUserListQuery request, CancellationToken cancellationToken)
        {
            try
            {
                Pagination<ApplicationUser> pagination = await _userRepository.GetUserListAsync(request.parameters);
                
                Pagination<UserRDTO> data = mapper.Map<Pagination<UserRDTO>>(pagination);

                return new ResponseRDTO<Pagination<UserRDTO>>
                {
                    StatusCode = 200,
                    Success = true,
                    Data = data
                };
            }
            catch(Exception ex)
            {

                return new ResponseRDTO<Pagination<UserRDTO>>
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
