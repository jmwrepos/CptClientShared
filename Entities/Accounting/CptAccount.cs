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
        public int AccountTypeId { get; set; }
        [Required]
        public virtual CptAcctType AccountType { get; set; } = null!;
        public virtual List<CptAcctUser> Users { get; set; } = new();
        [Required]
        public string AccountName { get; set; } = string.Empty;
        public byte[] EncryptionKey { get; set; } = Array.Empty<byte>();
        public virtual List<CptLibrary> Libraries { get; set; } = new();
        public bool Active { get; set; }
    }
}
