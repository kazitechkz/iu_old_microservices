using UserManagementService.API.Application.DTO.ResponseDTO;
using UserManagementService.API.Application.DTO.UserDTO;
using UserManagementService.API.Application.Parameters;
using UserManagementService.API.Domain.Models;

namespace UserManagementService.API.Application.Repositories
{
    public interface IUserRepository
    {
        public Task AddUserAsync(ApplicationUser user, string UserPassword, string RoleCode);

        public Task UpdateUserAsync(ApplicationUser user, string? UserPassword, string? RoleCode);

        public Task<Pagination<ApplicationUser>> GetUserListAsync(UserParameter parameter);
        public Task<ApplicationUser> GetUser(string UserId);

        public Task<bool> DeleteUser(ApplicationUser user);
    }
}
