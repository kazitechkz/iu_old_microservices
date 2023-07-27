using SubjectService.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SubjectService.Application.Contracts.ISpecifications
{
    public interface ISpecification<T> where T : BaseModel
    {

        Expression<Func<T, bool>> Criteria { get; }
        List<string> Includes { get; }
        Expression<Func<T, object>> OrderBy { get; }
        Expression<Func<T, object>> OrderByDescending { get; }
        int Take { get; }
        int Skip { get; }
        bool IsPagingEnabled { get; }

    }
}
