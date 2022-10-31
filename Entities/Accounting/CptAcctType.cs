using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CptClientShared.Entities.Accounting
{
    public class CptAcctType
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public virtual List<CptAccount> Accounts { get; set; } = new();
        public bool Active { get; set; }
    }
}
