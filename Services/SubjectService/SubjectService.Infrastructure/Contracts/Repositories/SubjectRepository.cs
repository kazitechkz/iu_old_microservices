using Microsoft.EntityFrameworkCore;
using SubjectService.Application.Contracts.IRepositories;
using SubjectService.Domain.Models;
using SubjectService.Infrastructure.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubjectService.Infrastructure.Contracts.Repositories
{
    public class SubjectRepository : GenericRepository<SubjectModel>, ISubjectRepository
    {
        private readonly ApplicationDbContext _context;
        public SubjectRepository(ApplicationDbContext context) : base(context)
        {
            this._context = context;
        }

        public async Task<SubjectModel> GetByCodeAsync(string code)
        {
            return await _context.Subjects.FirstOrDefaultAsync(p => p.Code.Equals(code));
        }
    }
}
