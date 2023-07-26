using AutoMapper;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Application.Contracts.IRepositories;
using UserManagement.Application.DTO.GenderDTO;
using UserManagement.Application.DTO.ResponseDTO;
using UserManagement.Domain;
using UserManagement.Domain.Models;

namespace UserManagement.Application.Features.Gender
{
    public class GenderCreateCommand : IRequest<ResponseRDTO<GenderRDTO>>
    {
        public GenderCDTO model { get; set; }

        public GenderCreateCommand(GenderCDTO model)
        {
            this.model = model;
        }
    }

    public class GenderCreateCommandHandler : IRequestHandler<GenderCreateCommand, ResponseRDTO<GenderRDTO>>
    {

        private readonly IGenderRepository genderRepository;
        private readonly IMapper mapper;
        private readonly AppConfig appConfig;

        public GenderCreateCommandHandler(IGenderRepository genderRepository, IMapper mapper,AppConfig appConfig)
        {
            this.genderRepository = genderRepository;
            this.mapper = mapper;
            this.appConfig = appConfig;
        }

        public async Task<ResponseRDTO<GenderRDTO>> Handle(GenderCreateCommand request, CancellationToken cancellationToken)
        {
            try
            {
                GenderModel entity = mapper.Map<GenderModel>(request.model);
                entity =  await genderRepository.AddAsync(entity);
                return new ResponseRDTO<GenderRDTO>
                {
                    StatusCode = 200,
                    Success = true,
                    Data = mapper.Map<GenderRDTO>(entity)
                };
            }
            catch(Exception ex)
            {

                return new ResponseRDTO<GenderRDTO>
                {
                    StatusCode = 500,
                    Success = false,
                    Message = "Something went wrong",
                    Detail = (appConfig.IsDevelopement == true ? ex.ToString() : "")
                };
            }
        }
    }

    public class GenderCreateCommandValidator : AbstractValidator<GenderCreateCommand>
    {
        public GenderCreateCommandValidator()
        {
            RuleFor(p => p.model.TitleEn).NotEmpty().WithMessage(x => "Not Empty").MaximumLength(255).WithMessage("Max Length 255").OverridePropertyName("TitleEn");
            RuleFor(p => p.model.TitleRu).NotEmpty().WithMessage(x => "Not Empty").MaximumLength(255).WithMessage("Max Length 255").OverridePropertyName("TitleRu");
            RuleFor(p => p.model.TitleKk).NotEmpty().WithMessage(x => "Not Empty").MaximumLength(255).WithMessage("Max Length 255").OverridePropertyName("TitleKk");
            RuleFor(p => p.model.Status).NotEmpty().WithMessage(x => "Not Empty").OverridePropertyName("Status");
            RuleFor(p => p.model.Code).NotEmpty().WithMessage(x => "Not Empty").MaximumLength(255).WithMessage("Max Length 255").OverridePropertyName("Code");
        }
    }
}
