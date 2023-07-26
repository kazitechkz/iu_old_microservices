using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Application.Contracts.IRepositories;
using UserManagement.Application.DTO.GenderDTO;
using UserManagement.Application.DTO.ResponseDTO;
using UserManagement.Application.DTO.RoleDTO;
using UserManagement.Domain.Models;

namespace UserManagement.Application.Features.Role
{
    public class RoleGetByIdQuery : IRequest<ResponseRDTO<RoleRDTO>>
    {
        public long Id { get; set; }
        public RoleGetByIdQuery(long Id)
        {
            this.Id = Id;
        }
    }

    public class RoleGetByIdQueryHandler : IRequestHandler<RoleGetByIdQuery, ResponseRDTO<RoleRDTO>>
    {

        private readonly IRoleRepository repository;
        private readonly IMapper mapper;

        public RoleGetByIdQueryHandler(IRoleRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<ResponseRDTO<RoleRDTO>> Handle(RoleGetByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                RoleModel entity = await repository.GetByIdAsync(request.Id);
                if (entity == null)
                {
                    return new ResponseRDTO<RoleRDTO>
                    {
                        StatusCode = 404,
                        Success = true,
                        Message = "Not Found"
                    };
                }
                return new ResponseRDTO<RoleRDTO>
                {
                    StatusCode = 200,
                    Success = true,
                    Data = mapper.Map<RoleRDTO>(entity)
                };
            }
            catch (Exception ex)
            {

                return new ResponseRDTO<RoleRDTO>
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
