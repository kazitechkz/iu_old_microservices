using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Application.Contracts.ISpecifications;
using UserManagement.Domain.Models;

namespace UserManagement.Infrastructure.Contracts.Specifications
{
    public class Specification<T> : ISpecification<T> where T : BaseModel
    {
        public Specification()
        {

        }

        public Specification(Expression<Func<T, bool>> _Criteria)
        {
            Criteria = _Criteria;
        }

        protected void AddInclude(string includeExpression)
        {
            Includes.Add(includeExpression);
        }

        protected void AddOrderBy(Expression<Func<T, object>> orderByExpression)
        {
            OrderBy = orderByExpression;
        }

        protected void AddOrderByDescending(Expression<Func<T, object>> orderByDescExpression)
        {
            OrderByDescending = orderByDescExpression;
        }

        protected void ApplyPaging(int skip, int take)
        {
            Skip = skip;
            Take = take;
            IsPagingEnabled = true;
        }

        public Expression<Func<T, bool>> Criteria { get; }
        public List<string> Includes { get; } = new List<string>();
        public Expression<Func<T, object>> OrderBy { get; private set; }
        public Expression<Func<T, object>> OrderByDescending { get; private set; }
        public int Take { get; private set; }
        public int Skip { get; private set; }
        public bool IsPagingEnabled { get; private set; }
    }
}
