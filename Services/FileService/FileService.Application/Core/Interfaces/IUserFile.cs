using FileService.Application.Core.DTOs.UserFiles;
using FileService.Domain.Models;

namespace FileService.Application.Core.Interfaces;

public interface IUserFile : IGeneric<UserFile>
{
    // public Task<UserFileRDTO> GetFileByIiiAndUserId(string iin, long userId);
}