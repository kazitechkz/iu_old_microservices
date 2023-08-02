using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace IdentityServer.Models
{
    public class ApplicationUser : IdentityUser
    {
       
        public string Name { get; set; }
        public string Surname { get; set; }

        public string? Middlename { get; set; }

        public string? ImageUrl { get; set; }

        public DateOnly BirthDate { get; set; }

        public int Status { get; set; }

        public long? GenderId { get; set; }

        public DateTime CreatedAt { get; set; }


        public DateTime? UpdatedAt { get; set; }

        public DateTime? DeletedAt { get; set; }

        [DefaultValue(false)]
        public bool IsDeleted { get; set; }

    }
}
