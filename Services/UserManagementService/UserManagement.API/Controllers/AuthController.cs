using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using UserManagement.Application.DTO.AuthDTO;
using UserManagement.Application.DTO.ResponseDTO;
using UserManagement.Application.DTO.UserDTO;
using UserManagement.Application.Features.Auth;
using UserManagement.Application.Features.User;

namespace UserManagement.API.Controllers
{
    public class AuthController : BaseApiController
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType(typeof(ResponseRDTO<string>), (int)HttpStatusCode.Created)]
        public async Task<ActionResult<ResponseRDTO<string>>> Login([FromBody] LoginDTO model)
        {
            ResponseRDTO<string> result = await _mediator.Send(new LoginCommand(model));
            return StatusCode(result.StatusCode, result);
        }
    }
}
