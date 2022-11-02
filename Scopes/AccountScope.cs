using CptClientShared.Entities;
using CptClientShared.Entities.Accounting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CptClientShared.Scopes
{
    public class AccountScope
    {
        public int Id { get; set; }
        public int AccountTypeId { get; set; }
        public virtual List<UserScope> Users { get; set; } = new();
        public string AccountName { get; set; } = string.Empty;
        public virtual List<string> Libraries { get; set; } = new();
        public bool Active { get; set; }

        public AccountScope(CptAccount acct)
        {
            Id = acct.Id;
            AccountTypeId = acct.AccountTypeId;
            Users = new();
            foreach(CptAcctUser user in acct.Users)
            {
                Users.Add(new(user));
            }
            AccountName = acct.AccountName;
            Libraries = new();
            foreach(CptLibrary lib in acct.Libraries)
            {
                Libraries.Add(lib.Name);
            }
            Active = acct.Active;
        }
    }
}
