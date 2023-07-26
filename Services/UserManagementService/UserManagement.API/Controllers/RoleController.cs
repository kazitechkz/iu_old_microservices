using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using UserManagement.Application.DTO.GenderDTO;
using UserManagement.Application.DTO.ResponseDTO;
using UserManagement.Application.DTO.RoleDTO;
using UserManagement.Application.Features.Gender;
using UserManagement.Application.Features.Role;

namespace UserManagement.API.Controllers
{
    public class RoleController : BaseApiController
    {
        private readonly IMediator _mediator;

        public RoleController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ResponseRDTO<RoleRDTO>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ResponseRDTO<RoleRDTO>>> Get([FromQuery] long Id)
        {
            ResponseRDTO<RoleRDTO> result = await _mediator.Send(new RoleGetByIdQuery(Id));
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet]
        [ProducesResponseType(typeof(ResponseRDTO<IReadOnlyList<RoleRDTO>>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ResponseRDTO<IReadOnlyList<RoleRDTO>>>> All()
        {
            ResponseRDTO<IReadOnlyList<RoleRDTO>> result = await _mediator.Send(new RoleListQuery());
            return StatusCode(result.StatusCode, result);
        }

        [HttpPut]
        [ProducesResponseType(typeof(ResponseRDTO<RoleRDTO>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ResponseRDTO<RoleRDTO>>> Update([FromBody] RoleUDTO model, [FromQuery] long Id)
        {
            ResponseRDTO<RoleRDTO> result = await _mediator.Send(new RoleUpdateCommand(model, Id));
            return StatusCode(result.StatusCode, result);
        }

    }
}
