namespace FileService.Domain.Models;

public class UserFile : BaseModel
{
    public string Title { get; set; }
    public string IIN { get; set; }
    public long UserId { get; set; }
    public long UploadFileId { get; set; }
    public UploadFile UploadFile { get; set; }
}