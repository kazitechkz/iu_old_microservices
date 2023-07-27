using SubjectService.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubjectService.Application.Contracts.IRepositories
{
    public interface IGradeLevelRepository : IGenericRepository<GradeLevelModel>
    {
        public Task<GradeLevelModel> GetByCodeAsync(string code);


    }
}
