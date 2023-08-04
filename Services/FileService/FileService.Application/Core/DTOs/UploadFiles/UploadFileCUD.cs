using FileService.Domain.Models;
using Microsoft.AspNetCore.Http;

namespace FileService.Application.Core.DTOs.UploadFiles;

public class UploadFileCUD
{
    public string Url { get; set; }
}