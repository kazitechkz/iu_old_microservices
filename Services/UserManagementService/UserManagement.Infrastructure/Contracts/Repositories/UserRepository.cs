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
    public class UserRepository : GenericRepository<UserModel>, IUserRepository
    {
        private readonly ApplicationDbContext _context;
        public UserRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<UserModel> GetUserByEmail(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(p=>p.Email.ToLower().Equals(email.ToLower()));
        }

        public async Task<UserModel> GetUserByPhone(string phone)
        {
            return await _context.Users.FirstOrDefaultAsync(p => p.Phone.ToLower().Equals(phone.ToLower()));
        }
    }
}
