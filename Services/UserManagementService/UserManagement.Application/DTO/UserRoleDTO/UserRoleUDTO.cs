using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Application.DTO.BaseDTO;
using UserManagement.Application.DTO.RoleDTO;
using UserManagement.Application.DTO.UserDTO;

namespace UserManagement.Application.DTO.UserRoleDTO
{
    public class UserRoleUDTO
    {
        public long UserId { get; set; }

        public long RoleId { get; set; }

        public long? SchoolId { get; set; }

        public long? AcademicYearId { get; set; }

        public DateOnly StartAt { get; set; }

        public DateOnly EndAt { get; set; }

        public int Status { get; set; }
    }
}
