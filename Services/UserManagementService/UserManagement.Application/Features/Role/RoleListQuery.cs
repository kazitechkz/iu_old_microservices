using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Application.Contracts.IRepositories;
using UserManagement.Application.DTO.ResponseDTO;
using UserManagement.Application.DTO.RoleDTO;
using UserManagement.Domain.Models;

namespace UserManagement.Application.Features.Role
{
    public class RoleListQuery : IRequest<ResponseRDTO<IReadOnlyList<RoleRDTO>>>
    {
        
    }

    public class RoleListQueryHandler : IRequestHandler<RoleListQuery,ResponseRDTO<IReadOnlyList<RoleRDTO>>>
    {
        private readonly IRoleRepository repository;
        private readonly IMapper mapper;

        public RoleListQueryHandler(IRoleRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<ResponseRDTO<IReadOnlyList<RoleRDTO>>> Handle(RoleListQuery request, CancellationToken cancellationToken)
        {
            try
            {
                IReadOnlyList<RoleModel> entity = await repository.ListAllAsync();

                return new ResponseRDTO<IReadOnlyList<RoleRDTO>>
                {
                    StatusCode = 200,
                    Success = true,
                    Data = mapper.Map<IReadOnlyList<RoleRDTO>>(entity)
                };
            }
            catch (Exception ex)
            {

                return new ResponseRDTO<IReadOnlyList<RoleRDTO>>
                {
                    StatusCode = 500,
                    Success = false,
                    Message = "Something went wrong",
                    Detail = ex.ToString()
                };
            }
        }
    }

}
