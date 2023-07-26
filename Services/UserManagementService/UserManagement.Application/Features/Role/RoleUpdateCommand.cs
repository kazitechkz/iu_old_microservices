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
    public class RoleUpdateCommand : IRequest<ResponseRDTO<RoleRDTO>>
    {

        public RoleUDTO model { get; set; }
        public long Id { get; set; }

        public RoleUpdateCommand(RoleUDTO model, long Id)
        {
            this.model = model;
            this.Id = Id;
        }
    }


    public class RoleUpdateCommandHandler : IRequestHandler<RoleUpdateCommand,ResponseRDTO<RoleRDTO>>
    {
        private readonly IRoleRepository repository;
        private readonly IMapper mapper;

        public RoleUpdateCommandHandler(IRoleRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<ResponseRDTO<RoleRDTO>> Handle(RoleUpdateCommand request, CancellationToken cancellationToken)
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
                entity = mapper.Map<RoleUDTO, RoleModel>(request.model, entity);
                entity = await repository.UpdateAsync(entity);
                return new ResponseRDTO<RoleRDTO>
                {
                    StatusCode = 201,
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
