using System.Net;
using Microsoft.AspNetCore.Mvc;
using OrganizationService.Application.Core;
using OrganizationService.Application.Core.DTOs.Schools;
using OrganizationService.Application.Features.Schools;
using OrganizationService.Infrastructure.Specifications.SchoolSpecifications;

namespace OrganizationService.API.Controllers;

public class SchoolController : BaseController
{
    [HttpGet]
    [ProducesResponseType(typeof(PagedList<SchoolRDTO>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetSchools([FromQuery] SchoolParameters parameters)
    {
        var specification = new SchoolSpecification();
        return HandlePagedResult(await Mediator.Send(new ListQuery.Query{specification = specification, parameters = parameters}));
    }

    [HttpGet("id")]
    [ProducesResponseType(typeof(SchoolRDTO), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetSchoolById(long id)
    {
        return HandleResult(await Mediator.Send(new DetailQuery.Query {Id = id}));
    }

    [HttpPost]
    [ProducesResponseType(typeof(SchoolRDTO), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> CreateSchool(SchoolCUD schoolCud)
    {
        return HandleResult(await Mediator.Send(new CreateCommand.Command {schoolCud = schoolCud}));
    }
    
    [HttpPut("id")]
    [ProducesResponseType(typeof(SchoolRDTO), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> UpdateSchool(long id, SchoolCUD schoolCud)
    {
        return HandleResult(await Mediator.Send(new EditCommand.Command {Id = id, schoolCud = schoolCud}));
    }

    [HttpDelete("id")]
    [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> DeleteSchool(long id)
    {
        return HandleResult(await Mediator.Send(new DeleteCommand.Command {Id = id}));
    }
}