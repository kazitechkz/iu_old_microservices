using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Application.Contracts.Parameters;

namespace UserManagement.Infrastructure.Contracts.Parameters.UserRoleParameters
{
    public class UserRoleParameter : BaseParameter
    {
        public DateOnly ActualDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);
        public int Status { get; set; } = 1;

    }
}
