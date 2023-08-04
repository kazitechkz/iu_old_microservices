using System.ComponentModel;

namespace UserManagementService.API.Application.DTO.UserDTO
{
    public class UserRDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }

        public string IIN { get; set; }

        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public bool EmailConfirmed { get; set; }

        public string UserName { get; set; }

        public string? Middlename { get; set; }

        public string? ImageUrl { get; set; }

        public DateOnly BirthDate { get; set; }

        public int Status { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        [DefaultValue(false)]
        public bool IsDeleted { get; set; }


    }
}
