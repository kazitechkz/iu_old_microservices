using System.ComponentModel;

namespace FileService.Domain.Models;

public class UploadFile : BaseModel
{
    public string Url { get; set; }
}