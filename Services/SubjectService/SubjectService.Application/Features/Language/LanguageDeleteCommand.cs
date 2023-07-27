using AutoMapper;
using FluentValidation;
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
    public class LanguageDeleteCommand : IRequest<ResponseRDTO<bool>>
    {
        public LanguageDeleteCommand( long id)
        {
            Id = id;
        }

        public long Id { get; set; }


    }

    public class LanguageDeleteCommandHandler : IRequestHandler<LanguageDeleteCommand, ResponseRDTO<bool>>
    {
        private readonly ILanguageRepository repository;
        private readonly IMapper mapper;

        public LanguageDeleteCommandHandler(ILanguageRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<ResponseRDTO<bool>> Handle(LanguageDeleteCommand request, CancellationToken cancellationToken)
        {
            try
            {

                LanguageModel entity = await repository.GetByIdAsync(request.Id);
                if (entity == null)
                {
                    return new ResponseRDTO<bool>
                    {
                        StatusCode = 404,
                        Success = false,
                        Message = "Такое значение не найдено",
                    };
                }
                
                await repository.DeleteAsync(entity);
                return new ResponseRDTO<bool>
                {
                    StatusCode = 200,
                    Success = true,
                    Data = true
                };
            }
            catch (Exception ex)
            {

                return new ResponseRDTO<bool>
                {
                    StatusCode = 500,
                    Success = false,
                    Message = "Something went wrong",
                    Detail = ex.ToString()
                };
            }
        }
    }

    public class LanguageDeleteCommandValidator : AbstractValidator<LanguageDeleteCommand>
    {
        public LanguageDeleteCommandValidator()
        {
            RuleFor(p => p.Id).NotNull().WithMessage(x => "Not Null").OverridePropertyName("Id");
        }
    }
}
