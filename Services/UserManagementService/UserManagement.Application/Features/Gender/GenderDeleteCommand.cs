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
    public class GenderDeleteCommand : IRequest<ResponseRDTO<bool>>
    {
        public long Id { get; set; }

        public GenderDeleteCommand( long Id)
        {
            this.Id = Id;
        }
    }

    public class GenderDeleteCommandHandler : IRequestHandler<GenderDeleteCommand, ResponseRDTO<bool>>
    {
        
        private readonly IGenderRepository genderRepository;
        private readonly IMapper mapper;
        private readonly AppConfig appConfig;

        public GenderDeleteCommandHandler(IGenderRepository genderRepository, IMapper mapper, AppConfig appConfig)
        {
            this.genderRepository = genderRepository;
            this.mapper = mapper;
            this.appConfig = appConfig;
        }

        public async Task<ResponseRDTO<bool>> Handle(GenderDeleteCommand request, CancellationToken cancellationToken)
        {
            try
            {
                GenderModel entity = await genderRepository.GetByIdAsync(request.Id);
                if (entity == null)
                {
                    return new ResponseRDTO<bool>
                    {
                        StatusCode = 404,
                        Success = true,
                        Message = "Not Found"
                    };
                }
                await genderRepository.DeleteAsync(entity);
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
                    Detail = (appConfig.IsDevelopement == true ? ex.ToString() : "")
                };
            }
        }
    }
    public class GenderDeleteCommandValidator : AbstractValidator<GenderDeleteCommand>
    {
        public GenderDeleteCommandValidator()
        {
            RuleFor(p => p.Id).NotNull().WithMessage(x => "Not Null").OverridePropertyName("Id");
        }
    }
}
