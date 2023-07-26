using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;
using UserManagement.Application.DTO.GenderDTO;
using UserManagement.Application.DTO.ResponseDTO;
using UserManagement.Application.Features.Gender;

namespace UserManagement.API.Controllers
{
    public class GenderController : BaseApiController
    {
        private readonly IMediator _mediator;

        public GenderController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ResponseRDTO<GenderRDTO>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ResponseRDTO<GenderRDTO>>> Get([FromQuery] long Id)
        {
            ResponseRDTO<GenderRDTO> result = await _mediator.Send(new GenderGetByIdQuery(Id));
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet]
        [ProducesResponseType(typeof(ResponseRDTO<IReadOnlyList<GenderRDTO>>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ResponseRDTO<IReadOnlyList<GenderRDTO>>>> All()
        {
            ResponseRDTO <IReadOnlyList<GenderRDTO>> result = await _mediator.Send(new GenderListQuery());
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(ResponseRDTO<GenderRDTO>), (int)HttpStatusCode.Created)]
        public async Task<ActionResult<ResponseRDTO<GenderRDTO>>> Create([FromBody] GenderCDTO model)
        {
            ResponseRDTO<GenderRDTO> result = await _mediator.Send(new GenderCreateCommand(model));
            return StatusCode(result.StatusCode, result);
        }
        [HttpPut]
        [ProducesResponseType(typeof(ResponseRDTO<GenderRDTO>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ResponseRDTO<GenderRDTO>>> Update([FromBody] GenderUDTO model, [FromQuery] long Id)
        {
            ResponseRDTO<GenderRDTO> result = await _mediator.Send(new GenderUpdateCommand(model,Id));
            return StatusCode(result.StatusCode, result);
        }

        [HttpDelete]
        [ProducesResponseType(typeof(ResponseRDTO<bool>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ResponseRDTO<bool>>> Delete([FromQuery] long Id)
        {
            ResponseRDTO<bool> result = await _mediator.Send(new GenderDeleteCommand(Id));
            return StatusCode(result.StatusCode, result);
        }

    }
}
