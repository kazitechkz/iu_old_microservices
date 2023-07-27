using GradeLevelService.Application.Features.GradeLevel;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SubjectService.Application.DTO.GradeLevelDTO;
using SubjectService.Application.DTO.ResponseDTO;
using SubjectService.Application.Features.GradeLevel;
using System.Net;

namespace SubjectService.API.Controllers
{
    public class GradeLevelController : BaseApiController
    {
        private readonly IMediator _mediator;

        public GradeLevelController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ResponseRDTO<GradeLevelRDTO>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ResponseRDTO<GradeLevelRDTO>>> Get([FromQuery] long Id)
        {
            ResponseRDTO<GradeLevelRDTO> result = await _mediator.Send(new GradeLevelGetByIdQuery(Id));
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet]
        [ProducesResponseType(typeof(ResponseRDTO<IReadOnlyList<GradeLevelRDTO>>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ResponseRDTO<IReadOnlyList<GradeLevelRDTO>>>> All()
        {
            ResponseRDTO<IReadOnlyList<GradeLevelRDTO>> result = await _mediator.Send(new GradeLevelListQuery());
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(ResponseRDTO<GradeLevelRDTO>), (int)HttpStatusCode.Created)]
        public async Task<ActionResult<ResponseRDTO<GradeLevelRDTO>>> Create([FromBody] GradeLevelCDTO model)
        {
            ResponseRDTO<GradeLevelRDTO> result = await _mediator.Send(new GradeLevelCreateCommand(model));
            return StatusCode(result.StatusCode, result);
        }
        [HttpPut]
        [ProducesResponseType(typeof(ResponseRDTO<GradeLevelRDTO>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ResponseRDTO<GradeLevelRDTO>>> Update([FromBody] GradeLevelUDTO model, [FromQuery] long Id)
        {
            ResponseRDTO<GradeLevelRDTO> result = await _mediator.Send(new GradeLevelUpdateCommand(model, Id));
            return StatusCode(result.StatusCode, result);
        }

        [HttpDelete]
        [ProducesResponseType(typeof(ResponseRDTO<bool>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ResponseRDTO<bool>>> Delete([FromQuery] long Id)
        {
            ResponseRDTO<bool> result = await _mediator.Send(new GradeLevelDeleteCommand(Id));
            return StatusCode(result.StatusCode, result);
        }

    }
}
