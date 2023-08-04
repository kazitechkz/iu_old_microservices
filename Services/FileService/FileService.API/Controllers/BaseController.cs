using MediatR;
using Microsoft.AspNetCore.Mvc;
using FileService.API.Extensions;
using FileService.Application.Core;

namespace FileService.API.Controllers;
[DisableRequestSizeLimit]
[ApiController]
[Route("api/[controller]/[action]")]
public class BaseController : ControllerBase
{
    private IMediator _mediator;

    protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

    protected ActionResult HandleResult<T>(Response<T> result)
    {
        if (result == null) return NotFound();
        if (result.IsSuccess && result.Value != null)
            return Ok(result.Value);
        if (result.IsSuccess && result.Value == null)
            return NotFound();
        return BadRequest(result);
    }

    protected ActionResult HandlePagedResult<T>(Response<PagedList<T>> result)
    {
        if (result == null) return NotFound();
        if (result.IsSuccess && result.Value != null)
        {
            Response.AddPaginationHeader(result.Value.CurrentPage, result.Value.PageSize,
                result.Value.TotalCount, result.Value.TotalPages);
            return Ok(result.Value);
        }

        if (result.IsSuccess && result.Value == null)
            return NotFound();
        return BadRequest(result.Error);
    }
}