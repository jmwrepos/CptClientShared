using CptClientShared.Entities.Accounting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CptClientShared.Services.ServiceEntities
{
    internal class CptAuthResult
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public CpSession Session { get; set; } = new();

        internal void Okay(bool success, CptAcctUser user, string msg)
        {
            Success = success;
            Session.SetUser(user);
            Message = msg;
        }

        internal void Fail()
        {
            Message = "Failed to Authenticate.";
        }
    }
}
