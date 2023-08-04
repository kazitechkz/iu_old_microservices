using FileService.Domain.Models;

namespace FileService.Application.Core.DTOs.UserFiles;

public class UserFileRDTO : BaseDTO
{
    public string Title { get; set; }
    public string IIN { get; set; }
    public long UserId { get; set; }
    public long UploadFileId { get; set; }
    public virtual UploadFile UploadFile { get; set; }
}