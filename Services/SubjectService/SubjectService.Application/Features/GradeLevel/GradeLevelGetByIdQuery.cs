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
    public class GradeLevelGetByIdQuery : IRequest<ResponseRDTO<GradeLevelRDTO>>
    {
        public GradeLevelGetByIdQuery(long id)
        {
            Id = id;
        }

        public long Id { get; set; }


    }

    public class GradeLevelGetByIdQueryHandler : IRequestHandler<GradeLevelGetByIdQuery, ResponseRDTO<GradeLevelRDTO>>
    {
        private readonly IGradeLevelRepository repository;
        private readonly IMapper mapper;

        public GradeLevelGetByIdQueryHandler(IGradeLevelRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<ResponseRDTO<GradeLevelRDTO>> Handle(GradeLevelGetByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {

                GradeLevelModel entity = await repository.GetByIdAsync(request.Id);
                if (entity == null)
                {
                    return new ResponseRDTO<GradeLevelRDTO>
                    {
                        StatusCode = 404,
                        Success = false,
                        Message = "Такое значение не найдено",
                    };
                }

                return new ResponseRDTO<GradeLevelRDTO>
                {
                    StatusCode = 200,
                    Success = true,
                    Data = mapper.Map<GradeLevelRDTO>(entity)
                };
            }
            catch (Exception ex)
            {

                return new ResponseRDTO<GradeLevelRDTO>
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
