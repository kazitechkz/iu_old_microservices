using SubjectService.Application.Contracts.ISpecifications;
using SubjectService.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubjectService.Application.Contracts.IRepositories
{
    public interface IGenericRepository<T> where T : BaseModel
    {
        //Queries
        //Get<Name>ByIdAsync
        Task<T> GetByIdAsync(long id);
        //List<Name>AllAsync
        Task<IReadOnlyList<T>> ListAllAsync();
        //Get<Name>WithSpecAsync
        Task<T> GetEntityWithSpecAsync(ISpecification<T> spec);
        //List<Name>WithSpecAsync
        Task<IReadOnlyList<T>> ListWithSpecAsync(ISpecification<T> spec);
        //Count<Name>Async
        Task<int> CountAsync(ISpecification<T> spec);

        //Command
        //Add<Name>
        Task<T> AddAsync(T entity);
        //Update<Name>
        Task<T> UpdateAsync(T entity);
        //Delete<Name>
        Task<bool> DeleteAsync(T entity);
    }
}
