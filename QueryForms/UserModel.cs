using CptClientShared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CptClientShared.QueryForms
{
    public class UserModel
    {
        public string AccountName { get; set; }
        public string LibraryName { get; set; }
        public List<CptTypeCfg> UserTypes { get; set; }
        public UserModel(DbConfig2 config)
        {
            AccountName = config.AccountName;
            LibraryName = AccountName + "Lib";
            UserTypes = new()
            {
                new CptTypeCfg("CpUser", null, new(){
                    "FirstName",
                    "LastName",
                    "Username",
                    "Password",
                    "IV",
                    "AddrStreet",
                    "AddrLine2",
                    "City",
                    "State",
                    "Zip",
                    "PrimaryPhone",
                    "UserRole"
                }),
            };
        }
    }
}
