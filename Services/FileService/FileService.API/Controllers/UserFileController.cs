using FileService.Application.Core.DTOs.UserFiles;
using FileService.Application.Features.UserFiles;
using Microsoft.AspNetCore.Mvc;

namespace FileService.API.Controllers;

public class UserFileController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> CreateUserFile([FromForm] FromFormDTO formDto)
    {
        return HandleResult(await Mediator.Send(new CreateCommand.Command {FromFormDto = formDto}));
    }
}