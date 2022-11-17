using CptClientShared.Entities.Structure;
using CptClientShared.QueryForms;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CptClientShared.Entities.Accounting
{
    public class CptAccount
    {
        public int Id { get; set; }
        public virtual List<CptAcctUser> Users { get; set; } = new();
        [Required]
        public string AccountName { get; set; } = string.Empty;
        public byte[] EncryptionKey { get; set; } = Array.Empty<byte>();
        public virtual List<CptLibrary> Libraries { get; set; } = new();
        public bool Active { get; set; }
        public CptAccount()
        {

        }
        internal void Configure(DbConfig2 cfg)
        {
            EncryptionKey = ApiEncryption.NewKey();
            CptAcctUser acctUser = new()
            {
                Account = this,
                Active = true,
                Email = cfg.Email,
                FirstName = cfg.FirstName,
                LastName = cfg.LastName,
                UserIV = ApiEncryption.NewIV(),
            };
            Users.Add(acctUser);
            acctUser.SetPassword(EncryptionKey, cfg.Password);


            CptLibrary newLibrary = new()
            {
                Account = this,
                Name = cfg.LibraryName
            };
            Libraries.Add(newLibrary);
            newLibrary.Configure(cfg);
        }
    }
}
