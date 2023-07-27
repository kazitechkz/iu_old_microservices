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
    public class LanguageRepository : GenericRepository<LanguageModel>, ILanguageRepository
    {
        private readonly ApplicationDbContext _context;
        public LanguageRepository(ApplicationDbContext context) : base(context)
        {
            this._context = context;
        }

        public async Task<LanguageModel> GetByCodeAsync(string code)
        {
            return await _context.Languages.FirstOrDefaultAsync(p => p.Code.Equals(code));
        }
    }
}
