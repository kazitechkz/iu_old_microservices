using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrganizationService.API.Helpers;
using OrganizationService.Application.Core.Authorize;
using OrganizationService.Application.Core.DTOs.LegalForms;
using OrganizationService.Application.Features.LegalForms;

namespace OrganizationService.API.Controllers;

public class LegalFormController : BaseController
{
    [Authorize]
    [HttpGet]
    [ProducesResponseType(typeof(List<LegalFormRDTO>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetLegalForms()
    {
        return HandleResult(await Mediator.Send(new ListQuery.Query()));
    }

    [Authorize]
    [HttpGet("id")]
    [ProducesResponseType(typeof(LegalFormRDTO), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetLegalFormById(long id)
    {
        return HandleResult(await Mediator.Send(new DetailQuery.Query{Id = id}));
    }

    [AuthorizeByRole(AuthConstants.Superadmin)]
    [HttpPost]
    [ProducesResponseType(typeof(LegalFormRDTO), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> CreateLegalForm(LegalFormCUD legalFormCud)
    {
        return HandleResult(await Mediator.Send(new CreateCommand.Command {LegalFormCud = legalFormCud}));
    }

    [AuthorizeByRole(AuthConstants.Superadmin)]
    [HttpPut("id")]
    [ProducesResponseType(typeof(LegalFormRDTO), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> UpdateLegalForm(long id, LegalFormCUD legalFormCud)
    {
        return HandleResult(await Mediator.Send(new EditCommand.Command {Id = id, legalFormCud = legalFormCud}));
    }

    [AuthorizeByRole(AuthConstants.Superadmin)]
    [HttpDelete("id")]
    [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> DeleteLegalForm(long id)
    {
        return HandleResult(await Mediator.Send(new DeleteCommand.Command {Id = id}));
    }
}