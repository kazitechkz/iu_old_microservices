using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagement.Domain.Models
{
    public class ClientModel : BaseModel
    {
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }

        public string RoleCode { get; set; }
    }
}
