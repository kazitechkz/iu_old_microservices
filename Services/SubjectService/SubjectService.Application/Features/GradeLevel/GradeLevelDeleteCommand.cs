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
    public class GradeLevelDeleteCommand : IRequest<ResponseRDTO<bool>>
    {
        public GradeLevelDeleteCommand(long id)
        {
            Id = id;
        }

        public long Id { get; set; }


    }

    public class GradeLevelDeleteCommandHandler : IRequestHandler<GradeLevelDeleteCommand, ResponseRDTO<bool>>
    {
        private readonly IGradeLevelRepository repository;
        private readonly IMapper mapper;

        public GradeLevelDeleteCommandHandler(IGradeLevelRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<ResponseRDTO<bool>> Handle(GradeLevelDeleteCommand request, CancellationToken cancellationToken)
        {
            try
            {

                GradeLevelModel entity = await repository.GetByIdAsync(request.Id);
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

    public class GradeLevelDeleteCommandValidator : AbstractValidator<GradeLevelDeleteCommand>
    {
        public GradeLevelDeleteCommandValidator()
        {
            RuleFor(p => p.Id).NotNull().WithMessage(x => "Not Null").OverridePropertyName("Id");
        }
    }
}
