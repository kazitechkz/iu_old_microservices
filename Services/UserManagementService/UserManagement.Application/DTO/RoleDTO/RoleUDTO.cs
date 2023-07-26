using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Application.DTO.BaseDTO;

namespace UserManagement.Application.DTO.RoleDTO
{
    public class RoleUDTO
    {
        public string TitleRu { get; set; }
        public string TitleKk { get; set; }
        public string TitleEn { get; set; }
        
        public int Status { get; set; }
    }
}
