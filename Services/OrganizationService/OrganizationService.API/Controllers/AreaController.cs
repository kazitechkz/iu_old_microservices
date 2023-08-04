using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrganizationService.API.Helpers;
using OrganizationService.Application.Core.Authorize;
using OrganizationService.Application.Core.DTOs.Areas;
using OrganizationService.Application.Features.Areas;

namespace OrganizationService.API.Controllers;

public class AreaController : BaseController
{
    [HttpGet]
    [Authorize]
    [ProducesResponseType(typeof(List<AreaRDTO>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetAreas()
    {
        return HandleResult(await Mediator.Send(new ListQuery.Query()));
    }

    [HttpGet("id")]
    [Authorize]
    [ProducesResponseType(typeof(AreaRDTO), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetAreaById(long id)
    {
        return HandleResult(await Mediator.Send(new DetailQuery.Query{Id = id}));
    }

    [HttpPost]
    [AuthorizeByRole(AuthConstants.Superadmin)]
    [ProducesResponseType(typeof(AreaRDTO), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> CreateArea(AreaCUD areaCud)
    {
        return HandleResult(await Mediator.Send(new CreateCommand.Command {areaCud = areaCud}));
    }
    
    [HttpPut("id")]
    [AuthorizeByRole(AuthConstants.Superadmin)]
    [ProducesResponseType(typeof(AreaRDTO), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> UpdateArea(long id, AreaCUD areaCud)
    {
        return HandleResult(await Mediator.Send(new EditCommand.Command {Id = id, areaCud = areaCud}));
    }

    [HttpDelete("id")]
    [AuthorizeByRole(AuthConstants.Superadmin)]
    [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> DeleteArea(long id)
    {
        return HandleResult(await Mediator.Send(new DeleteCommand.Command {Id = id}));
    }
}