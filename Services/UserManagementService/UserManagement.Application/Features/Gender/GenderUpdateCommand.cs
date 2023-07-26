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
    public class GenderUpdateCommand : IRequest<ResponseRDTO<GenderRDTO>>
    {
        public GenderUDTO model { get; set; }
        public long Id { get; set; }

        public GenderUpdateCommand(GenderUDTO model,long Id)
        {
            this.model = model;
            this.Id = Id;
        }

    }

    public class GenderUpdateCommandHandler : IRequestHandler<GenderUpdateCommand, ResponseRDTO<GenderRDTO>>
    {
        private readonly IGenderRepository genderRepository;
        private readonly IMapper mapper;

        public GenderUpdateCommandHandler(IGenderRepository genderRepository, IMapper mapper)
        {
            this.genderRepository = genderRepository;
            this.mapper = mapper;
        }
        public async Task<ResponseRDTO<GenderRDTO>> Handle(GenderUpdateCommand request, CancellationToken cancellationToken)
        {
            try
            {
                GenderModel entity = await genderRepository.GetByIdAsync(request.Id);
                if(entity == null)
                {
                    return new ResponseRDTO<GenderRDTO>
                    {
                        StatusCode = 404,
                        Success = true,
                        Message = "Not Found"
                    };
                }
                entity = mapper.Map<GenderUDTO,GenderModel>(request.model,entity);
                entity = await genderRepository.UpdateAsync(entity);
                return new ResponseRDTO<GenderRDTO>
                {
                    StatusCode = 201,
                    Success = true,
                    Data = mapper.Map<GenderRDTO>(entity)
                };
            }
            catch (Exception ex)
            {

                return new ResponseRDTO<GenderRDTO>
                {
                    StatusCode = 500,
                    Success = false,
                    Message = "Something went wrong",
                    Detail = ex.ToString()
                };
            }
        }
    }

    public class GenderUpdateCommandValidator : AbstractValidator<GenderUpdateCommand>
    {
        public GenderUpdateCommandValidator()
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
