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
    public class SubjectListQuery : IRequest<ResponseRDTO<IReadOnlyList<SubjectRDTO>>>
    {


    }

    public class SubjectListQueryHandler : IRequestHandler<SubjectListQuery, ResponseRDTO<IReadOnlyList<SubjectRDTO>>>
    {
        private readonly ISubjectRepository repository;
        private readonly IMapper mapper;

        public SubjectListQueryHandler(ISubjectRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<ResponseRDTO<IReadOnlyList<SubjectRDTO>>> Handle(SubjectListQuery request, CancellationToken cancellationToken)
        {
            try
            {

                IReadOnlyList<SubjectModel> entity = await repository.ListAllAsync();


                return new ResponseRDTO<IReadOnlyList<SubjectRDTO>>
                {
                    StatusCode = 200,
                    Success = true,
                    Data = mapper.Map<IReadOnlyList<SubjectRDTO>>(entity)
                };
            }
            catch (Exception ex)
            {

                return new ResponseRDTO<IReadOnlyList<SubjectRDTO>>
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
