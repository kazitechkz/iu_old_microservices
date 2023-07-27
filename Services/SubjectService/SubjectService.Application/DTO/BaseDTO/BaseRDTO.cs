using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubjectService.Application.DTO.BaseDTO
{
    public class BaseRDTO
    {

        public long Id { get; set; }

        public DateTime CreatedAt { get; set; }


        public DateTime? UpdatedAt { get; set; }

        public DateTime? DeletedAt { get; set; }


        public bool IsDeleted { get; set; }
    }
}
