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
    public class LanguageCreateCommand : IRequest<ResponseRDTO<LanguageRDTO>>
    {

        public LanguageCDTO model { get; set; }

        public LanguageCreateCommand(LanguageCDTO model)
        {
            this.model = model;
        }
    }

    public class LanguageCreateCommandHandler : IRequestHandler<LanguageCreateCommand, ResponseRDTO<LanguageRDTO>>
    {
        private readonly ILanguageRepository repository;
        private readonly IMapper mapper;

        public LanguageCreateCommandHandler(ILanguageRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<ResponseRDTO<LanguageRDTO>> Handle(LanguageCreateCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if((await repository.GetByCodeAsync(request.model.Code)) != null)
                {
                    return new ResponseRDTO<LanguageRDTO>
                    {
                        StatusCode = 400,
                        Success = false,
                        Message = "Такое значение уже существует",
                    };
                }
                LanguageModel entity = mapper.Map<LanguageModel>(request.model);
                entity = await repository.AddAsync(entity);
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

    public class LanguageCreateCommandValidator : AbstractValidator<LanguageCreateCommand>
    {
        public LanguageCreateCommandValidator()
        {
            RuleFor(p => p.model.TitleEn).NotEmpty().WithMessage(x => "Not Empty").MaximumLength(255).WithMessage("Max Length 255").OverridePropertyName("TitleEn");
            RuleFor(p => p.model.TitleRu).NotEmpty().WithMessage(x => "Not Empty").MaximumLength(255).WithMessage("Max Length 255").OverridePropertyName("TitleRu");
            RuleFor(p => p.model.TitleKk).NotEmpty().WithMessage(x => "Not Empty").MaximumLength(255).WithMessage("Max Length 255").OverridePropertyName("TitleKk");
            RuleFor(p => p.model.Status).NotEmpty().WithMessage(x => "Not Empty").OverridePropertyName("Status");
            RuleFor(p => p.model.Code).NotNull().WithMessage(x => "Not Empty").OverridePropertyName("Code");
        }
    }
}
