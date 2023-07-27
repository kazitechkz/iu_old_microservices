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
    public class LanguageListQuery : IRequest<ResponseRDTO<IReadOnlyList<LanguageRDTO>>>
    {
       

    }

    public class LanguageListQueryHandler : IRequestHandler<LanguageListQuery, ResponseRDTO<IReadOnlyList<LanguageRDTO>>>
    {
        private readonly ILanguageRepository repository;
        private readonly IMapper mapper;

        public LanguageListQueryHandler(ILanguageRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<ResponseRDTO<IReadOnlyList<LanguageRDTO>>> Handle(LanguageListQuery request, CancellationToken cancellationToken)
        {
            try
            {

                IReadOnlyList<LanguageModel> entity = await repository.ListAllAsync();
                

                return new ResponseRDTO<IReadOnlyList<LanguageRDTO>>
                {
                    StatusCode = 200,
                    Success = true,
                    Data = mapper.Map<IReadOnlyList<LanguageRDTO>> (entity)
                };
            }
            catch (Exception ex)
            {

                return new ResponseRDTO<IReadOnlyList<LanguageRDTO>>
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
