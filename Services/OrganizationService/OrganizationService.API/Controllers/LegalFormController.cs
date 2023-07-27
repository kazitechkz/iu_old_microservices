using System.Net;
using Microsoft.AspNetCore.Mvc;
using OrganizationService.Application.Core.DTOs.LegalForms;
using OrganizationService.Application.Features.LegalForms;

namespace OrganizationService.API.Controllers;

public class LegalFormController : BaseController
{
    [HttpGet]
    [ProducesResponseType(typeof(List<LegalFormRDTO>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetLegalForms()
    {
        return HandleResult(await Mediator.Send(new ListQuery.Query()));
    }

    [HttpGet("id")]
    [ProducesResponseType(typeof(LegalFormRDTO), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetLegalFormById(long id)
    {
        return HandleResult(await Mediator.Send(new DetailQuery.Query{Id = id}));
    }

    [HttpPost]
    [ProducesResponseType(typeof(LegalFormRDTO), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> CreateLegalForm(LegalFormCUD legalFormCud)
    {
        return HandleResult(await Mediator.Send(new CreateCommand.Command {LegalFormCud = legalFormCud}));
    }
    
    [HttpPut("id")]
    [ProducesResponseType(typeof(LegalFormRDTO), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> UpdateLegalForm(long id, LegalFormCUD legalFormCud)
    {
        return HandleResult(await Mediator.Send(new EditCommand.Command {Id = id, legalFormCud = legalFormCud}));
    }

    [HttpDelete("id")]
    [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> DeleteLegalForm(long id)
    {
        return HandleResult(await Mediator.Send(new DeleteCommand.Command {Id = id}));
    }
}