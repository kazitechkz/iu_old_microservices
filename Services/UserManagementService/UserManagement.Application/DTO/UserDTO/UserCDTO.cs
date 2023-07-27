using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagement.Application.DTO.UserDTO
{
    public class UserCDTO
    {
        public string Name { get; set; }
        public string Surname { get; set; }

        public string? MiddleName { get; set; }

        public string Phone { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public string? ImageUrl { get; set; }

        public DateOnly BirthDate { get; set; }

        public int Status { get; set; }

        public long? GenderId { get; set; }

        public string? Code { get; set; }

        public long? SchoolId { get; set; }

        public long? AcademicYearId { get; set; }
        public DateOnly? StartAt { get; set; }
        public DateOnly? EndAt { get; set; }


    }
}
