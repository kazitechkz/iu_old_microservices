using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubjectService.Domain.Models
{
    public class BaseModel : ISoftDelete
    {
        [Key]
        public long Id { get; set; }

        public DateTime CreatedAt { get; set; }


        public DateTime? UpdatedAt { get; set; }

        public DateTime? DeletedAt { get; set; }


        public bool IsDeleted { get; set; }
    }
}
