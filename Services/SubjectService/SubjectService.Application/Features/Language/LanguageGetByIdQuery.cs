using AutoMapper;
using MediatR;
using SubjectService.Application.Contracts.IRepositories;
using SubjectService.Application.DTO.LanguageDTO;
using SubjectService.Application.DTO.ResponseDTO;
using SubjectService.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubjectService.Application.Features.Language
{
    public class LanguageGetByIdQuery : IRequest<ResponseRDTO<LanguageRDTO>>
    {
        public LanguageGetByIdQuery(long id)
        {
            Id = id;
        }

        public long Id { get; set; }


    }

    public class LanguageGetByIdQueryHandler : IRequestHandler<LanguageGetByIdQuery, ResponseRDTO<LanguageRDTO>>
    {
        private readonly ILanguageRepository repository;
        private readonly IMapper mapper;

        public LanguageGetByIdQueryHandler(ILanguageRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<ResponseRDTO<LanguageRDTO>> Handle(LanguageGetByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {

                LanguageModel entity = await repository.GetByIdAsync(request.Id);
                if (entity == null)
                {
                    return new ResponseRDTO<LanguageRDTO>
                    {
                        StatusCode = 404,
                        Success = false,
                        Message = "Такое значение не найдено",
                    };
                }

                return new ResponseRDTO<LanguageRDTO>
                {
                    StatusCode = 200,
                    Success = true,
                    Data = mapper.Map<LanguageRDTO>(entity)
                };
            }
            catch (Exception ex)
            {

                return new ResponseRDTO<LanguageRDTO>
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
