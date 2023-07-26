using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagement.Domain.Models
{
    public interface ISoftDelete
    {
        public DateTime? DeletedAt { get; set; }

        public bool IsDeleted { get; set; }

        public void UndoDelete()
        {
            DeletedAt = null;
            IsDeleted = false;
        }


    }
}
