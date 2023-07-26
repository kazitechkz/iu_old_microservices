using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagement.Domain.Models
{
    public class UserRoleModel : BaseModel
    {
        public long UserId { get; set; }

        public UserModel User { get; set; }
        public long RoleId { get; set; }

        public RoleModel Role { get; set; }

        public long? SchoolId { get; set;}

        public long? AcademicYearId { get; set; }

        public DateOnly StartAt { get; set; }

        public DateOnly EndAt { get; set; }

        [DefaultValue(1)]
        public int Status { get; set; }

    }
}
