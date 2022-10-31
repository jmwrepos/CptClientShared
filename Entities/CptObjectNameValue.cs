using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CptClientShared.Entities
{
    public class CptObjectNameValue
    {
        public int Id { get; set; }
        public string Value { get; set; }
        public virtual int OwnerId { get; set; }
        public virtual CptObjectProperty Owner { get; set; }
        public CptObjectNameValue()
        {
            Value = String.Empty;
            Owner = null!;
        }
        public CptObjectNameValue(string value)
        {
            Value = value;
            Owner = null!;
        }
    }
}
