using AutoMapper;
using FluentValidation;
using MediatR;
using SubjectService.Application.Contracts.IRepositories;
using SubjectService.Application.DTO.GradeLevelDTO;
using SubjectService.Application.DTO.LanguageDTO;
using SubjectService.Application.DTO.ResponseDTO;
using SubjectService.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubjectService.Application.Features.GradeLevel
{
    
    public class GradeLevelCreateCommand : IRequest<ResponseRDTO<GradeLevelRDTO>>
    {

        public GradeLevelCDTO model { get; set; }

        public GradeLevelCreateCommand(GradeLevelCDTO model)
        {
            this.model = model;
        }
    }

    public class GradeLevelCreateCommandHandler : IRequestHandler<GradeLevelCreateCommand, ResponseRDTO<GradeLevelRDTO>>
    {
        private readonly IGradeLevelRepository repository;
        private readonly IMapper mapper;

        public GradeLevelCreateCommandHandler(IGradeLevelRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<ResponseRDTO<GradeLevelRDTO>> Handle(GradeLevelCreateCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if ((await repository.GetByCodeAsync(request.model.Code)) != null)
                {
                    return new ResponseRDTO<GradeLevelRDTO>
                    {
                        StatusCode = 400,
                        Success = false,
                        Message = "Такое значение уже существует",
                    };
                }
                GradeLevelModel entity = mapper.Map <GradeLevelModel>(request.model);
                entity = await repository.AddAsync(entity);
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

    public class GradeLevelCreateCommandValidator : AbstractValidator<GradeLevelCreateCommand>
    {
        public GradeLevelCreateCommandValidator()
        {
            RuleFor(p => p.model.TitleEn).NotEmpty().WithMessage(x => "Not Empty").MaximumLength(255).WithMessage("Max Length 255").OverridePropertyName("TitleEn");
            RuleFor(p => p.model.TitleRu).NotEmpty().WithMessage(x => "Not Empty").MaximumLength(255).WithMessage("Max Length 255").OverridePropertyName("TitleRu");
            RuleFor(p => p.model.TitleKk).NotEmpty().WithMessage(x => "Not Empty").MaximumLength(255).WithMessage("Max Length 255").OverridePropertyName("TitleKk");
            RuleFor(p => p.model.Status).NotEmpty().WithMessage(x => "Not Empty").OverridePropertyName("Status");
            RuleFor(p => p.model.Code).NotNull().WithMessage(x => "Not Empty").OverridePropertyName("Code");
        }
    }
}
