using System.Net;
using AcademicYearService.Application.Core;
using AcademicYearService.Application.Core.DTOs.Terms;
using AcademicYearService.Application.Features.Terms;
using AcademicYearService.Infrastructure.Specifications.TermSpecifications;
using Microsoft.AspNetCore.Mvc;

namespace AcademicYearService.API.Controllers;

public class TermController : BaseController
{
    [HttpGet]
    [ProducesResponseType(typeof(PagedList<TermRDTO>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetTerms([FromQuery] TermParameters parameters)
    {
        var spec = new TermSpecification();
        return HandlePagedResult(await Mediator.Send(new ListQuery.Query {specification = spec, parameters = parameters}));
    }

    [HttpPost]
    [ProducesResponseType(typeof(TermRDTO), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> CreateTerm(TermCUD termCud)
    {
        return HandleResult(await Mediator.Send(new CreateCommand.Command {TermCud = termCud}));
    }

    [HttpPut("id")]
    [ProducesResponseType(typeof(TermRDTO), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> UpdateTerm(long id, TermCUD termCud)
    {
        return HandleResult(await Mediator.Send(new EditCommand.Command {Id = id, TermCud = termCud}));
    }

    [HttpDelete("id")]
    [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> DeleteTerm(long id)
    {
        return HandleResult(await Mediator.Send(new DeleteCommand.Command { Id = id }));
    }
}