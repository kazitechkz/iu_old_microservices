using AutoMapper;
using MediatR;
using SubjectService.Application.Contracts.IRepositories;
using SubjectService.Application.DTO.GradeLevelDTO;
using SubjectService.Application.DTO.ResponseDTO;
using SubjectService.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradeLevelService.Application.Features.GradeLevel
{
    public class GradeLevelListQuery : IRequest<ResponseRDTO<IReadOnlyList<GradeLevelRDTO>>>
    {


    }

    public class GradeLevelListQueryHandler : IRequestHandler<GradeLevelListQuery, ResponseRDTO<IReadOnlyList<GradeLevelRDTO>>>
    {
        private readonly IGradeLevelRepository repository;
        private readonly IMapper mapper;

        public GradeLevelListQueryHandler(IGradeLevelRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<ResponseRDTO<IReadOnlyList<GradeLevelRDTO>>> Handle(GradeLevelListQuery request, CancellationToken cancellationToken)
        {
            try
            {

                IReadOnlyList<GradeLevelModel> entity = await repository.ListAllAsync();


                return new ResponseRDTO<IReadOnlyList<GradeLevelRDTO>>
                {
                    StatusCode = 200,
                    Success = true,
                    Data = mapper.Map<IReadOnlyList<GradeLevelRDTO>>(entity)
                };
            }
            catch (Exception ex)
            {

                return new ResponseRDTO<IReadOnlyList<GradeLevelRDTO>>
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
