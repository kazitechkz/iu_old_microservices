using System.Net;
using AcademicYearService.Application.Core;
using AcademicYearService.Application.Core.DTOs.AcademicYears;
using AcademicYearService.Application.Features.AcademicYears;
using AcademicYearService.Infrastructure.Specifications.AcademicYearSpecifications;
using Microsoft.AspNetCore.Mvc;

namespace AcademicYearService.API.Controllers;

public class AcademicYearController : BaseController
{
    [HttpGet]
    [ProducesResponseType(typeof(PagedList<AcademicYearRDTO>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetAcademicYears([FromQuery] AcademicYearParameters parameters)
    {
        var spec = new AcademicYearSpecification();
        return HandlePagedResult(await Mediator.Send(new ListQuery.Query {specification = spec, parameters = parameters}));
    }

    [HttpPost]
    [ProducesResponseType(typeof(AcademicYearRDTO), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> CreateAcademicYear(AcademicYearCUD academicYearCud)
    {
        return HandleResult(await Mediator.Send(new CreateCommand.Command {AcademicYearCud = academicYearCud}));
    }

    [HttpPut("id")]
    [ProducesResponseType(typeof(AcademicYearRDTO), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> UpdateAcademicYear(long id, AcademicYearCUD academicYearCud)
    {
        return HandleResult(await Mediator.Send(new EditCommand.Command {Id = id, AcademicYearCud = academicYearCud}));
    }

    [HttpDelete("id")]
    [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> DeleteAcademicYear(long id)
    {
        return HandleResult(await Mediator.Send(new DeleteCommand.Command { Id = id }));
    }
}