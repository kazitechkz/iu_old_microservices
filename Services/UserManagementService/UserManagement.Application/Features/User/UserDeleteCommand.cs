using AutoMapper;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Application.Contracts.IRepositories;
using UserManagement.Application.DTO.ResponseDTO;
using UserManagement.Application.DTO.UserDTO;
using UserManagement.Application.Helpers;
using UserManagement.Domain.Models;

namespace UserManagement.Application.Features.User
{
    public class UserDeleteCommand : IRequest<ResponseRDTO<bool>>
    {
        public UserDeleteCommand( long Id)
        {
            this.Id = Id;
        }
        public long Id { get; set; }
    }

    public class UserDeleteCommandHandler : IRequestHandler<UserDeleteCommand, ResponseRDTO<bool>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserRoleRepository _userRoleRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;

        public UserDeleteCommandHandler(IUserRepository userRepository, IUserRoleRepository userRoleRepository, IRoleRepository roleRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _userRoleRepository = userRoleRepository;
            _roleRepository = roleRepository;
            _mapper = mapper;
        }

        public async Task<ResponseRDTO<bool>> Handle(UserDeleteCommand request, CancellationToken cancellationToken)
        {
            try
            {

                UserModel entity = await _userRepository.GetByIdAsync(request.Id);
                if (entity == null)
                {
                    return new ResponseRDTO<bool>
                    {
                        StatusCode = 404,
                        Message = "Not Found",
                        Success = false
                    };
                }
                await _userRepository.DeleteAsync(entity);
                return new ResponseRDTO<bool>
                {
                    StatusCode = 201,
                    Success = true,
                    Data = true
                };

            }
            catch (Exception ex)
            {
                return new ResponseRDTO<bool>
                {
                    StatusCode = 500,
                    Message = ex.Message,
                    Success = false,
                    Detail = ex.ToString(),
                };
            }
        }
    }

    public class UserDeleteValidator : AbstractValidator<UserDeleteCommand>
    {
        public UserDeleteValidator()
        {
            RuleFor(p => p.Id)
          .NotNull().WithMessage("Field is Required")
          .OverridePropertyName("Id");
        }



    }
}
