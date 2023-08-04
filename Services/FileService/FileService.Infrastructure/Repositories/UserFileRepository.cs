using FileService.Application.Core.DTOs.UserFiles;
using FileService.Application.Core.Interfaces;
using FileService.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace FileService.Infrastructure.Repositories;

public class UserFileRepository : Generic<UserFile>, IUserFile
{
    private readonly DataContext _context;

    public UserFileRepository(DataContext context) : base(context)
    {
        _context = context;
    }

    // public async Task<UserFileRDTO> GetFileByIiiAndUserId(string iin, long userId)
    // {
    //     return await _context.Set<UserFile>().AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
    // }
}