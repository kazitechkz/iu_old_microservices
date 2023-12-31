﻿using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SubjectService.API.Helpers;
using SubjectService.Application.Authorize;
using SubjectService.Application.DTO.LanguageDTO;
using SubjectService.Application.DTO.ResponseDTO;
using SubjectService.Application.Features.Language;
using System.Net;

namespace SubjectService.API.Controllers
{
    public class LanguageController : BaseApiController
    {
        private readonly IMediator _mediator;

        public LanguageController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize]
        [HttpGet]
        [ProducesResponseType(typeof(ResponseRDTO<LanguageRDTO>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ResponseRDTO<LanguageRDTO>>> Get([FromQuery] long Id)
        {
            ResponseRDTO<LanguageRDTO> result = await _mediator.Send(new LanguageGetByIdQuery(Id));
            return StatusCode(result.StatusCode, result);
        }

        [Authorize]
        [HttpGet]
        [ProducesResponseType(typeof(ResponseRDTO<IReadOnlyList<LanguageRDTO>>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ResponseRDTO<IReadOnlyList<LanguageRDTO>>>> All()
        {
            ResponseRDTO<IReadOnlyList<LanguageRDTO>> result = await _mediator.Send(new LanguageListQuery());
            return StatusCode(result.StatusCode, result);
        }

        [AuthorizeByRole(AuthConstants.Superadmin, AuthConstants.Methodist)]
        [HttpPost]
        [ProducesResponseType(typeof(ResponseRDTO<LanguageRDTO>), (int)HttpStatusCode.Created)]
        public async Task<ActionResult<ResponseRDTO<LanguageRDTO>>> Create([FromBody] LanguageCDTO model)
        {
            ResponseRDTO<LanguageRDTO> result = await _mediator.Send(new LanguageCreateCommand(model));
            return StatusCode(result.StatusCode, result);
        }

        [AuthorizeByRole(AuthConstants.Superadmin, AuthConstants.Methodist)]
        [HttpPut]
        [ProducesResponseType(typeof(ResponseRDTO<LanguageRDTO>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ResponseRDTO<LanguageRDTO>>> Update([FromBody] LanguageUDTO model, [FromQuery] long Id)
        {
            ResponseRDTO<LanguageRDTO> result = await _mediator.Send(new LanguageUpdateCommand(model, Id));
            return StatusCode(result.StatusCode, result);
        }

        [AuthorizeByRole(AuthConstants.Superadmin, AuthConstants.Methodist)]
        [HttpDelete]
        [ProducesResponseType(typeof(ResponseRDTO<bool>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ResponseRDTO<bool>>> Delete([FromQuery] long Id)
        {
            ResponseRDTO<bool> result = await _mediator.Send(new LanguageDeleteCommand(Id));
            return StatusCode(result.StatusCode, result);
        }

    }
}
