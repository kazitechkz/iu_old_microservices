using AutoMapper;
using FluentValidation;
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
    public class GradeLevelUpdateCommand : IRequest<ResponseRDTO<GradeLevelRDTO>>
    {
        public GradeLevelUpdateCommand(GradeLevelUDTO model, long id)
        {
            this.model = model;
            Id = id;
        }

        public GradeLevelUDTO model { get; set; }
        public long Id { get; set; }


    }

    public class GradeLevelUpdateCommandHandler : IRequestHandler<GradeLevelUpdateCommand, ResponseRDTO<GradeLevelRDTO>>
    {
        private readonly IGradeLevelRepository repository;
        private readonly IMapper mapper;

        public GradeLevelUpdateCommandHandler(IGradeLevelRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<ResponseRDTO<GradeLevelRDTO>> Handle(GradeLevelUpdateCommand request, CancellationToken cancellationToken)
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
                var old = await repository.GetByCodeAsync(request.model.Code);
                if (old != null && old.Id != request.Id)
                {
                    return new ResponseRDTO<GradeLevelRDTO>
                    {
                        StatusCode = 400,
                        Success = false,
                        Message = "Такое значение уже существует",
                    };
                }
                entity = mapper.Map<GradeLevelUDTO, GradeLevelModel>(request.model, entity);
                entity = await repository.UpdateAsync(entity);
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

    public class GradeLevelUpdateCommandValidator : AbstractValidator<GradeLevelUpdateCommand>
    {
        public GradeLevelUpdateCommandValidator()
        {
            RuleFor(p => p.Id).NotNull().WithMessage(x => "Not Null").OverridePropertyName("Id");
            RuleFor(p => p.model.TitleEn).NotEmpty().WithMessage(x => "Not Empty").MaximumLength(255).WithMessage("Max Length 255").OverridePropertyName("TitleEn");
            RuleFor(p => p.model.TitleRu).NotEmpty().WithMessage(x => "Not Empty").MaximumLength(255).WithMessage("Max Length 255").OverridePropertyName("TitleRu");
            RuleFor(p => p.model.TitleKk).NotEmpty().WithMessage(x => "Not Empty").MaximumLength(255).WithMessage("Max Length 255").OverridePropertyName("TitleKk");
            RuleFor(p => p.model.Status).NotEmpty().WithMessage(x => "Not Empty").OverridePropertyName("Status");
            RuleFor(p => p.model.Code).NotNull().WithMessage(x => "Not Empty").OverridePropertyName("Code");
        }
    }
}
