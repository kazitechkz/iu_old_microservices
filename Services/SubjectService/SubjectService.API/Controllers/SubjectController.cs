﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using SubjectService.Application.DTO.SubjectDTO;
using SubjectService.Application.DTO.ResponseDTO;
using SubjectService.Application.Features.Subject;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using SubjectService.API.Helpers;
using SubjectService.Application.Authorize;

namespace SubjectService.API.Controllers
{
    public class SubjectController : BaseApiController
    {
        private readonly IMediator _mediator;

        public SubjectController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize]
        [HttpGet]
        [ProducesResponseType(typeof(ResponseRDTO<SubjectRDTO>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ResponseRDTO<SubjectRDTO>>> Get([FromQuery] long Id)
        {
            ResponseRDTO<SubjectRDTO> result = await _mediator.Send(new SubjectGetByIdQuery(Id));
            return StatusCode(result.StatusCode, result);
        }

        [Authorize]
        [HttpGet]
        [ProducesResponseType(typeof(ResponseRDTO<IReadOnlyList<SubjectRDTO>>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ResponseRDTO<IReadOnlyList<SubjectRDTO>>>> All()
        {
            ResponseRDTO<IReadOnlyList<SubjectRDTO>> result = await _mediator.Send(new SubjectListQuery());
            return StatusCode(result.StatusCode, result);
        }

        [AuthorizeByRole(AuthConstants.Superadmin, AuthConstants.Methodist)]
        [HttpPost]
        [ProducesResponseType(typeof(ResponseRDTO<SubjectRDTO>), (int)HttpStatusCode.Created)]
        public async Task<ActionResult<ResponseRDTO<SubjectRDTO>>> Create([FromBody] SubjectCDTO model)
        {
            ResponseRDTO<SubjectRDTO> result = await _mediator.Send(new SubjectCreateCommand(model));
            return StatusCode(result.StatusCode, result);
        }

        [AuthorizeByRole(AuthConstants.Superadmin, AuthConstants.Methodist)]
        [HttpPut]
        [ProducesResponseType(typeof(ResponseRDTO<SubjectRDTO>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ResponseRDTO<SubjectRDTO>>> Update([FromBody] SubjectUDTO model, [FromQuery] long Id)
        {
            ResponseRDTO<SubjectRDTO> result = await _mediator.Send(new SubjectUpdateCommand(model, Id));
            return StatusCode(result.StatusCode, result);
        }

        [AuthorizeByRole(AuthConstants.Superadmin, AuthConstants.Methodist)]
        [HttpDelete]
        [ProducesResponseType(typeof(ResponseRDTO<bool>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ResponseRDTO<bool>>> Delete([FromQuery] long Id)
        {
            ResponseRDTO<bool> result = await _mediator.Send(new SubjectDeleteCommand(Id));
            return StatusCode(result.StatusCode, result);
        }

    }
}
