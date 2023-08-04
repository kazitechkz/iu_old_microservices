
using FileService.Domain.Models;
using Microsoft.AspNetCore.Http;

namespace FileService.Application.Core.DTOs.UploadFiles;

public class UploadFileRDTO : BaseDTO
{
    public string Url { get; set; }
}