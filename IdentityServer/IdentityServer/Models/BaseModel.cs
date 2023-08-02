using Microsoft.AspNetCore.Identity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace IdentityServer.Models
{
    public interface BaseModel
    {

        [Key]
        public long Id { get; set; }
        
        public DateTime CreatedAt { get; set; }


        public DateTime? UpdatedAt { get; set; }

        public DateTime? DeletedAt { get; set; }

        [DefaultValue(false)]
        public bool IsDeleted { get; set; }
    }
}
