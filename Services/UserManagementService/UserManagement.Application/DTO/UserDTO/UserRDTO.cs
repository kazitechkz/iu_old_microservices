using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Application.DTO.BaseDTO;
using UserManagement.Application.DTO.UserRoleDTO;
using UserManagement.Domain.Models;

namespace UserManagement.Application.DTO.UserDTO
{
    public class UserRDTO : BaseRDTO
    {
        public string Name { get; set; }
        public string Surname { get; set; }

        public string? MiddleName { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public string? ImageUrl { get; set; }

        public DateOnly BirthDate { get; set; }

        public int Status { get; set; }

        public long? GenderId { get; set; }

        public IReadOnlyCollection<UserRoleRDTO> UserRoles { get; set; }



    }
}
