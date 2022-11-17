using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CptClientShared.QueryForms
{
    public class QfAuthenticateUser
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public QfAuthenticateUser(string email, string password)
        {
            Email = email;
            Password = password;
        }
    }
}
