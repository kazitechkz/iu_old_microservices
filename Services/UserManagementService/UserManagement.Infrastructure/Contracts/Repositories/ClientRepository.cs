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
    public class ClientRepository : GenericRepository<ClientModel>, IClientRepository
    {
        private readonly ApplicationDbContext _context;
        public ClientRepository(ApplicationDbContext context) : base(context)
        {
            this._context = context;
        }

        public async Task<ClientModel> GetByClientId(string clientId)
        {
            return await _context.Clients.FirstOrDefaultAsync(p=>p.ClientId.Equals(clientId));
        }
    }
}
