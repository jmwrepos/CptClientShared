using CptClientShared.Entities.Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CptClientShared.Entities.Vals
{
    public class CptBytesValue
    {
        public int Id { get; set; }
        public byte[] Value { get; set; }
        public virtual int OwnerId { get; set; }
        public virtual CptObjectProperty Owner { get; set; }
        public CptBytesValue()
        {
            Owner = null!;
            Value = Array.Empty<byte>();
        }
        public CptBytesValue(byte[] value)
        {
            Value = value;
            Owner = null!;
        }
    }
}
