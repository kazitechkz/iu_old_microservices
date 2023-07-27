using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Domain.Models;
using UserManagement.Infrastructure.Contracts.Parameters.UserParameters;

namespace UserManagement.Infrastructure.Contracts.Specifications.UserSpecifications
{
    public class UserSpecification : Specification<UserModel>
    {

        public UserSpecification()
        {
               
        }

        public UserSpecification(long Id) : base(p=>p.Id.Equals(Id)) {
            AddInclude("UserRoles.Role");
        }


        public UserSpecification(UserParameters parameter,bool isCount = false) : base(a=>
            (string.IsNullOrEmpty(parameter.Search) || 
             a.Name.ToLower().Contains(parameter.Search.ToLower()) ||
             a.Surname.ToLower().Contains(parameter.Search.ToLower()) ||
             a.MiddleName.ToLower().Contains(parameter.Search.ToLower()) ||
             a.Phone.ToLower().Contains(parameter.Search.ToLower()) ||
             a.Email.ToLower().Contains(parameter.Search.ToLower()))
            )
            {
            if(!isCount)
            {
                AddInclude("UserRoles.Role");
                ApplyPaging(parameter.PageSize * (parameter.PageIndex - 1), parameter.PageSize);
            }
        }




    }
}
