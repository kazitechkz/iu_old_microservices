using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace UserManagement.Application.Helpers
{
    public static class SecurityHelper
    {
       

        public static string EncryptPassword(string text)
        {
            return BCrypt.Net.BCrypt.HashPassword(text);
        }

        public static bool VerifyPassword(string password,string toComparison)
        {
            return BCrypt.Net.BCrypt.Verify(toComparison,password);
        }

    }
}
