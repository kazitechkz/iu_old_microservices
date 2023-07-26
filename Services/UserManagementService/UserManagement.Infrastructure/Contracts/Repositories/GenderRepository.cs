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
    public class GenderRepository : GenericRepository<GenderModel>, IGenderRepository
    {
        public GenderRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
