using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Domain.Models;
using UserManagement.Infrastructure.Contracts.Parameters.UserRoleParameters;

namespace UserManagement.Infrastructure.Contracts.Specifications.UserRoleSpecifications
{
    public class UserRoleSpecification : Specification<UserRoleModel>
    {
        public UserRoleSpecification()
        {
                
        }


        public UserRoleSpecification(long Id) : base(p=>p.Id.Equals(Id)) 
        {
                
       
        }

        public UserRoleSpecification(long UserId, UserRoleParameter parameter) : base(
            p => p.UserId.Equals(UserId) 
            &&
               (p.StartAt < parameter.ActualDate) 
            &&
                (p.EndAt > parameter.ActualDate)
            && 
            p.Status.Equals(parameter.Status)
            )
        {
            AddInclude("Role");

        }


    }
}
