using Microsoft.AspNetCore.Http;

namespace FileService.Application.Core.DTOs.UserFiles;

public class FromFormDTO : UserFileCUD
{
    public IFormFile File { get; set; }
}