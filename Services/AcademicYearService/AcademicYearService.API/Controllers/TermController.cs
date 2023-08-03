using System.Net;
using AcademicYearService.API.Helpers;
using AcademicYearService.Application.Core;
using AcademicYearService.Application.Core.Authorize;
using AcademicYearService.Application.Core.DTOs.Terms;
using AcademicYearService.Application.Features.Terms;
using AcademicYearService.Infrastructure.Specifications.TermSpecifications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AcademicYearService.API.Controllers;

public class TermController : BaseController
{
    [Authorize]
    [HttpGet]
    [ProducesResponseType(typeof(PagedList<TermRDTO>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetTerms([FromQuery] TermParameters parameters)
    {
        var spec = new TermSpecification();
        return HandlePagedResult(await Mediator.Send(new ListQuery.Query {specification = spec, parameters = parameters}));
    }

    [AuthorizeByRole(AuthConstants.Superadmin, AuthConstants.Methodist)]
    [HttpPost]
    [ProducesResponseType(typeof(TermRDTO), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> CreateTerm(TermCUD termCud)
    {
        return HandleResult(await Mediator.Send(new CreateCommand.Command {TermCud = termCud}));
    }

    [AuthorizeByRole(AuthConstants.Superadmin, AuthConstants.Methodist)]
    [HttpPut("id")]
    [ProducesResponseType(typeof(TermRDTO), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> UpdateTerm(long id, TermCUD termCud)
    {
        return HandleResult(await Mediator.Send(new EditCommand.Command {Id = id, TermCud = termCud}));
    }

    [AuthorizeByRole(AuthConstants.Superadmin, AuthConstants.Methodist)]
    [HttpDelete("id")]
    [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> DeleteTerm(long id)
    {
        return HandleResult(await Mediator.Send(new DeleteCommand.Command { Id = id }));
    }
}