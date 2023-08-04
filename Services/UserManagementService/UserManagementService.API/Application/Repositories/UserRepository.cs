using IdentityModel;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using UserManagementService.API.Application.DTO.ResponseDTO;
using UserManagementService.API.Application.DTO.UserDTO;
using UserManagementService.API.Application.Parameters;
using UserManagementService.API.Domain.Models;
using UserManagementService.API.Infrastructure.Database;

namespace UserManagementService.API.Application.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _appDbContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly AppDbContext context;

        public UserRepository(AppDbContext appDbContext, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, AppDbContext context)
        {
            _appDbContext = appDbContext;
            _userManager = userManager;
            _roleManager = roleManager;
            this.context = context;
        }

        public async Task AddUserAsync(ApplicationUser user, string UserPassword, string RoleCode)
        {
            await _userManager.CreateAsync(user, UserPassword);
            await _userManager.AddToRoleAsync(user, RoleCode);
            var superadmin_claim = await _userManager.AddClaimsAsync(user, new Claim[]
            {
                        new Claim(JwtClaimTypes.Role,RoleCode),
                        new Claim(JwtClaimTypes.Name,user.UserName),
                        new Claim(JwtClaimTypes.Email,user.Email),
                        new Claim("IIN",user.IIN),
            });
        }

        public async Task UpdateUserAsync(ApplicationUser user, string? UserPassword, string? RoleCode)
        {

            if (!string.IsNullOrEmpty(UserPassword))
            {
                string token = await _userManager.GeneratePasswordResetTokenAsync(user);
                await _userManager.ChangePasswordAsync(user, token, UserPassword);
            }
            if (!string.IsNullOrEmpty(RoleCode))
            {
                IList<string> old_roles = await _userManager.GetRolesAsync(user);
                await _userManager.RemoveFromRolesAsync(user, old_roles);
                await _userManager.AddToRoleAsync(user, RoleCode);
            }
            IList<Claim> claims = await _userManager.GetClaimsAsync(user);
            await _userManager.RemoveClaimsAsync(user, claims);
            await _userManager.UpdateAsync(user);
            IList<string> roles = await _userManager.GetRolesAsync(user);
            if (roles.Count > 0)
            {
                foreach (var role in roles)
                {
                    await _userManager.AddClaimsAsync(user, new Claim[]
                        {
                           new Claim(JwtClaimTypes.Role,role),
                           new Claim(JwtClaimTypes.Name,user.UserName),
                           new Claim(JwtClaimTypes.Email,user.Email),
                           new Claim("IIN",user.IIN),
                        });
                }

            }
        }

        public async Task<ApplicationUser> GetUser(string UserId)
        {
            return await _userManager.FindByIdAsync(UserId);
        }

        public async Task<bool> DeleteUser(ApplicationUser user)
        {
             await _userManager.DeleteAsync(user);
             return true;
        }

        public async Task<Pagination<ApplicationUser>> GetUserListAsync(UserParameter parameter)
        {
            int count = GetQuery(parameter).Count();
            ICollection<ApplicationUser> users = GetQuery(parameter).Skip(parameter.PageSize * (parameter.PageIndex - 1)).Take(parameter.PageSize).ToList();
            Pagination<ApplicationUser> data = new Pagination<ApplicationUser>(parameter.PageIndex,parameter.PageSize,count,users);
            return data;

        }

        private IQueryable<ApplicationUser> GetQuery(UserParameter parameter)
        {
            IQueryable<ApplicationUser> query = _userManager.Users.Where(p =>
                ((
                string.IsNullOrEmpty(parameter.Search) ||
                p.Name.ToLower().Contains(parameter.Search.ToLower()) ||
                 p.Surname.ToLower().Contains(parameter.Search.ToLower()) ||
                 p.Email.ToLower().Contains(parameter.Search.ToLower()) ||
                 p.IIN.ToLower().Contains(parameter.Search.ToLower()) ||
                 p.PhoneNumber.ToLower().Contains(parameter.Search.ToLower()))
                 &&
                 p.IsDeleted == parameter.IsDeleted));

            return query;

        }
    }
}
