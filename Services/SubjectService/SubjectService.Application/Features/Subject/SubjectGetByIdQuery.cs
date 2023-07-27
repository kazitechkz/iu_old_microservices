using AutoMapper;
using MediatR;
using SubjectService.Application.Contracts.IRepositories;
using SubjectService.Application.DTO.SubjectDTO;
using SubjectService.Application.DTO.ResponseDTO;
using SubjectService.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubjectService.Application.Features.Subject
{
    public class SubjectGetByIdQuery : IRequest<ResponseRDTO<SubjectRDTO>>
    {
        public SubjectGetByIdQuery(long id)
        {
            Id = id;
        }

        public long Id { get; set; }


    }

    public class SubjectGetByIdQueryHandler : IRequestHandler<SubjectGetByIdQuery, ResponseRDTO<SubjectRDTO>>
    {
        private readonly ISubjectRepository repository;
        private readonly IMapper mapper;

        public SubjectGetByIdQueryHandler(ISubjectRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<ResponseRDTO<SubjectRDTO>> Handle(SubjectGetByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {

                SubjectModel entity = await repository.GetByIdAsync(request.Id);
                if (entity == null)
                {
                    return new ResponseRDTO<SubjectRDTO>
                    {
                        StatusCode = 404,
                        Success = false,
                        Message = "Такое значение не найдено",
                    };
                }

                return new ResponseRDTO<SubjectRDTO>
                {
                    StatusCode = 200,
                    Success = true,
                    Data = mapper.Map<SubjectRDTO>(entity)
                };
            }
            catch (Exception ex)
            {

                return new ResponseRDTO<SubjectRDTO>
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
