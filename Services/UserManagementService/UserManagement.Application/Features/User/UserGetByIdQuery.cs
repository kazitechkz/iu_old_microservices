using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Application.Contracts.IRepositories;
using UserManagement.Application.Contracts.ISpecifications;
using UserManagement.Application.DTO.ResponseDTO;
using UserManagement.Application.DTO.UserDTO;
using UserManagement.Domain.Models;

namespace UserManagement.Application.Features.User
{
    public class UserGetByIdQuery : IRequest<ResponseRDTO<UserRDTO>>
    {
        public UserGetByIdQuery(ISpecification<UserModel> specification)
        {
            this.specification = specification;
        }

        public ISpecification<UserModel> specification { get; set; }
    }

    public class UserGetByIdQueryHandler : IRequestHandler<UserGetByIdQuery, ResponseRDTO<UserRDTO>>
    {

        private readonly IUserRepository _userRepository;
        private readonly IUserRoleRepository _userRoleRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;

        public UserGetByIdQueryHandler(IUserRepository userRepository, IUserRoleRepository userRoleRepository, IRoleRepository roleRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _userRoleRepository = userRoleRepository;
            _roleRepository = roleRepository;
            _mapper = mapper;
        }

        public async Task<ResponseRDTO<UserRDTO>> Handle(UserGetByIdQuery request, CancellationToken cancellationToken)
        {
            try {
                UserModel entity = await _userRepository.GetEntityWithSpecAsync(request.specification);
                if(entity == null)
                {
                    return new ResponseRDTO<UserRDTO>
                    {
                        StatusCode = 404,
                        Message = "Not Found",
                        Success = false
                    };
                }
                return new ResponseRDTO<UserRDTO>
                {
                    StatusCode = 200,
                    Success = true,
                    Data = _mapper.Map<UserRDTO>(entity)
                };
            }
            catch (Exception ex)
            {
                return new ResponseRDTO<UserRDTO>
                {
                    StatusCode = 500,
                    Message = ex.Message.ToString(),
                    Success = false,
                    Detail = ex.ToString()
                };


            }
        }
    }
}
