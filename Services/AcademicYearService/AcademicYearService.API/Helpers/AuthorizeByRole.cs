using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace AcademicYearService.API.Helpers
{
    public class AuthorizeByRole : AuthorizeAttribute
    {
        public AuthorizeByRole(params string[] roles) : base()
        {
            Roles = String.Join(",", roles);
        }
    }
}
