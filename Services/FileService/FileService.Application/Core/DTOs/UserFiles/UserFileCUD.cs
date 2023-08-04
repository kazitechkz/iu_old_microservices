namespace FileService.Application.Core.DTOs.UserFiles;

public class UserFileCUD
{
    public string Title { get; set; }
    public string IIN { get; set; }
    public long UserId { get; set; }
    public long? UploadFileId { get; set; }
}