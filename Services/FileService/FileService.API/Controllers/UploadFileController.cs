using System.Diagnostics;
using System.Net;
using AutoMapper;
using FileService.Application.Core.Interfaces;
using FileService.Application.Features.UploadFiles;
using FileService.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace FileService.API.Controllers;

public class UploadFileController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> UploadFile([FromForm] IFormFile file, string? subDirectory)
    {
        return HandleResult(await Mediator.Send(new CreateCommand.Command{File = file, SubDirectory = subDirectory}));
    }

    [HttpGet("id")]
    public async Task<IActionResult> GetFileById(long id)
    {
        return HandleResult(await Mediator.Send(new DetailQuery.Query {Id = id}));
    }

    [HttpDelete("id")]
    public async Task<IActionResult> DeleteFile(long id)
    {
        return HandleResult(await Mediator.Send(new DeleteCommand.Command{Id = id}));
    }
}