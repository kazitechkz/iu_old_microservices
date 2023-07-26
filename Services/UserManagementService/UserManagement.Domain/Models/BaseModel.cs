using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagement.Domain.Models
{
    public class BaseModel : ISoftDelete
    {
        [Key]
        public long Id { get; set; }

        public DateTime CreatedAt { get; set; }

        [DefaultValue(null)]
        public DateTime? UpdatedAt { get; set;}
        [DefaultValue(null)]
        public DateTime? DeletedAt { get; set; }

        [DefaultValue(false)]
        public bool IsDeleted { get; set; }
    }
}
