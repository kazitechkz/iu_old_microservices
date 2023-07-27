using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Application.Contracts.IRepositories;
using UserManagement.Application.Contracts.ISpecifications;
using UserManagement.Application.Contracts.Parameters;
using UserManagement.Application.DTO.ResponseDTO;
using UserManagement.Application.DTO.UserDTO;
using UserManagement.Domain.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace UserManagement.Application.Features.User
{
    public class UserListQuery : IRequest<ResponseRDTO<PagingRDTO<IReadOnlyCollection<UserRDTO>>>>
    {
        public ISpecification<UserModel> specification;
        public ISpecification<UserModel> count_specification;
        public BaseParameter parameters;

        public UserListQuery(ISpecification<UserModel> specification, ISpecification<UserModel> count_specification, BaseParameter parameters)
        {
            this.specification = specification;
            this.count_specification = count_specification;
            this.parameters = parameters;
        }
    }


    public class UserListQueryHandler : IRequestHandler<UserListQuery, ResponseRDTO<PagingRDTO<IReadOnlyCollection<UserRDTO>>>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserRoleRepository _userRoleRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;

        public UserListQueryHandler(IUserRepository userRepository, IUserRoleRepository userRoleRepository, IRoleRepository roleRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _userRoleRepository = userRoleRepository;
            _roleRepository = roleRepository;
            _mapper = mapper;
        }

        public async Task<ResponseRDTO<PagingRDTO<IReadOnlyCollection<UserRDTO>>>> Handle(UserListQuery request, CancellationToken cancellationToken)
        {
            try
            {
                IReadOnlyCollection<UserModel> entity = await _userRepository.ListWithSpecAsync(request.specification);
                int count = await _userRepository.CountAsync(request.count_specification);
                IReadOnlyCollection<UserRDTO> model = _mapper.Map<IReadOnlyList<UserRDTO>>(entity);
                return new ResponseRDTO<PagingRDTO<IReadOnlyCollection<UserRDTO>>>
                {
                    StatusCode = 200,
                    Success = true,
                    Data = new PagingRDTO<IReadOnlyCollection<UserRDTO>>(count, request.parameters.PageIndex, request.parameters.PageSize, model)

                };
            }
            catch (Exception ex)
            {
                return new ResponseRDTO<PagingRDTO<IReadOnlyCollection<UserRDTO>>>
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
