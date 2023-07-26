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
using UserManagement.Domain.Models;
using UserManagement.Domain;

namespace UserManagement.Application.Features.Gender
{
    public class GenderGetByIdQuery : IRequest<ResponseRDTO<GenderRDTO>>
    {
        public long Id { get; set; }
        public GenderGetByIdQuery( long Id)
        {
            this.Id = Id;
        }
    }

    public class GenderGetByIdQueryHandler : IRequestHandler<GenderGetByIdQuery, ResponseRDTO<GenderRDTO>>
    {
        private readonly IGenderRepository genderRepository;
        private readonly IMapper mapper;
        private readonly AppConfig appConfig;

        public GenderGetByIdQueryHandler(IGenderRepository genderRepository, IMapper mapper, AppConfig appConfig)
        {
            this.genderRepository = genderRepository;
            this.mapper = mapper;
            this.appConfig = appConfig;
        }
        public async Task<ResponseRDTO<GenderRDTO>> Handle(GenderGetByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                GenderModel entity = await genderRepository.GetByIdAsync(request.Id);
                if (entity == null)
                {
                    return new ResponseRDTO<GenderRDTO>
                    {
                        StatusCode = 404,
                        Success = true,
                        Message = "Not Found"
                    };
                }
                return new ResponseRDTO<GenderRDTO>
                {
                    StatusCode = 201,
                    Success = true,
                    Data = mapper.Map<GenderRDTO>(entity)
                };
            }
            catch (Exception ex)
            {

                return new ResponseRDTO<GenderRDTO>
                {
                    StatusCode = 500,
                    Success = false,
                    Message = "Something went wrong",
                    Detail = (appConfig.IsDevelopement == true ? ex.ToString() : "")
                };
            }
        }
    }
}
