using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using UserManagement.Application.DTO.GenderDTO;
using UserManagement.Application.DTO.ResponseDTO;
using UserManagement.Application.DTO.UserDTO;
using UserManagement.Application.Features.Gender;
using UserManagement.Application.Features.User;
using UserManagement.Infrastructure.Contracts.Parameters.UserParameters;
using UserManagement.Infrastructure.Contracts.Specifications.UserSpecifications;

namespace UserManagement.API.Controllers
{
    public class UserController : BaseApiController
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType(typeof(ResponseRDTO<UserRDTO>), (int)HttpStatusCode.Created)]
        public async Task<ActionResult<ResponseRDTO<UserRDTO>>> Create([FromBody] UserCDTO model)
        {
            ResponseRDTO<UserRDTO> result = await _mediator.Send(new UserCreateCommand(model));
            return StatusCode(result.StatusCode, result);
        }

        [HttpPut]
        [ProducesResponseType(typeof(ResponseRDTO<UserRDTO>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ResponseRDTO<UserRDTO>>> Update([FromBody] UserUDTO model, [FromQuery] long Id)
        {
            ResponseRDTO<UserRDTO> result = await _mediator.Send(new UserUpdateCommand(model,Id));
            return StatusCode(result.StatusCode, result);
        }
        [HttpDelete]
        [ProducesResponseType(typeof(ResponseRDTO<bool>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ResponseRDTO<UserRDTO>>> Delete([FromQuery] long Id)
        {
            ResponseRDTO<bool> result = await _mediator.Send(new UserDeleteCommand(Id));
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet]
        [ProducesResponseType(typeof(ResponseRDTO<UserRDTO>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ResponseRDTO<UserRDTO>>> Get([FromQuery] long Id)
        {
            ResponseRDTO<UserRDTO> result = await _mediator.Send(new UserGetByIdQuery(new UserSpecification(Id)));
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet]
        [ProducesResponseType(typeof(ResponseRDTO<PagingRDTO<IReadOnlyCollection<UserRDTO>>>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ResponseRDTO<PagingRDTO<IReadOnlyCollection<UserRDTO>>>>> List([FromQuery] UserParameters parameters)
        {
            ResponseRDTO<PagingRDTO<IReadOnlyCollection<UserRDTO>>> result = await _mediator.Send(new UserListQuery(
                new UserSpecification(parameters, false),
                new UserSpecification(parameters,true),
                parameters
                ));
            return StatusCode(result.StatusCode, result);
        }
    }
}
