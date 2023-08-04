using AutoMapper;
using FileService.Application.Core.DTOs.UploadFiles;
using FileService.Application.Core.Interfaces;
using FileService.Application.Helpers;
using FileService.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace FileService.Infrastructure.Repositories;

public class UploadFileRepository : Generic<UploadFile>, IUploadFile
{
    public UploadFileRepository(DataContext context) : base(context)
    {
    }

    private string _pathFile;

    public UploadFileCUD SaveFile(IFormFile formFile, string? subDirectory)
    {
        var extension = Path.GetExtension(formFile.FileName);
        var newName = Path.GetFileNameWithoutExtension(formFile.FileName);
        _pathFile = subDirectory != null ? $"{AppConstants.UploadsFolder}/{subDirectory}" : $"{AppConstants.UploadsFolder}";

        var filePath = Path.Combine(_pathFile, $"{newName}_{Guid.NewGuid()}{extension}");
        if (!Directory.Exists(_pathFile))
        {
            Directory.CreateDirectory(_pathFile);
        }
        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            formFile.CopyToAsync(stream);
        }

        var uploadFileCud = new UploadFileCUD { Url = filePath};
        return uploadFileCud;
    }
}