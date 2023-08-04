using FileService.Domain.Models;

namespace FileService.Infrastructure.Specifications.UserFileSpecifications;

public class UserFileSpec : BaseSpecification<UserFile>
{
    public UserFileSpec()
    {
        AddInclude("UploadFile");
    }
}