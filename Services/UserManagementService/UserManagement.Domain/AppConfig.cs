using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagement.Domain
{
    public class AppConfig
    {
        public string? Mode { get; set; }
        public string? OrganizationService { get; set; }

        public string? SecurityKey { get; set; }
        public string? ValidIssuer { get; set; }

    }
}
