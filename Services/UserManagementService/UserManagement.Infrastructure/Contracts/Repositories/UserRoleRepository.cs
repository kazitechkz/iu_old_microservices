using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Application.Contracts.IRepositories;
using UserManagement.Domain.Models;
using UserManagement.Infrastructure.Database;

namespace UserManagement.Infrastructure.Contracts.Repositories
{
    public class UserRoleRepository : GenericRepository<UserRoleModel>, IUserRoleRepository
    {
        private readonly ApplicationDbContext _context;
        public UserRoleRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IReadOnlyCollection<UserRoleModel>> GetActualUserRole(long UserId)
        {
            return await _context.UserRoles.Where(
                p =>p.UserId.Equals(UserId)
                    &&
                    p.Status.Equals(1)
                    &&
                    (p.StartAt < DateOnly.FromDateTime(DateTime.Now) && p.EndAt > DateOnly.FromDateTime(DateTime.Now))
                )
                .Include( p => p.Role )
                .ToListAsync();
        }
    }
}
