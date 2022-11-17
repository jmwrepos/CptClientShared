using CptClientShared.Entities.Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CptClientShared.Entities.Vals
{
    public class CptBoolValues
    {
        public int Id { get; set; }
        public bool Value { get; set; }
        public virtual int OwnerId { get; set; }
        public virtual CptObjectProperty Owner { get; set; }
        public CptBoolValues()
        {
            Owner = null!;
            Value = false;
        }
        public CptBoolValues(bool value)
        {
            Value = value;
            Owner = null!;
        }
    }
}
