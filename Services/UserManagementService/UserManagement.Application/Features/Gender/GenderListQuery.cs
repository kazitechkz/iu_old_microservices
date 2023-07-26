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
using UserManagement.Domain;
using UserManagement.Domain.Models;

namespace UserManagement.Application.Features.Gender
{
    public class GenderListQuery : IRequest<ResponseRDTO<IReadOnlyList<GenderRDTO>>>
    {

    }


    public class GenderListQueryHandler : IRequestHandler<GenderListQuery, ResponseRDTO<IReadOnlyList<GenderRDTO>>>
    {
        private readonly IGenderRepository genderRepository;
        private readonly IMapper mapper;
        private readonly AppConfig appConfig;

        public GenderListQueryHandler(IGenderRepository genderRepository, IMapper mapper)
        {
            this.genderRepository = genderRepository;
            this.mapper = mapper;
        }

        public async Task<ResponseRDTO<IReadOnlyList<GenderRDTO>>> Handle(GenderListQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var entity = await genderRepository.ListAllAsync();
                return new ResponseRDTO<IReadOnlyList<GenderRDTO>>
                {
                    StatusCode = 200,
                    Success = true,
                    Data = mapper.Map<IReadOnlyList<GenderRDTO>>(entity)
                
                };
                
            }
            catch (Exception ex)
            {
                return new ResponseRDTO<IReadOnlyList<GenderRDTO>>
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
