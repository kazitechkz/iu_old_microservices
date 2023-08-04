using FileService.Application.Core.DTOs.UploadFiles;
using FileService.Domain.Models;
using Microsoft.AspNetCore.Http;

namespace FileService.Application.Core.Interfaces;

public interface IUploadFile : IGeneric<UploadFile>
{
    public UploadFileCUD SaveFile(IFormFile file, string? subDirectory);
}