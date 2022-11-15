using CptClientShared.Entities.Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CptClientShared.Entities.Vals
{
    public class BoolValues
    {
        public int Id { get; set; }
        public bool Value { get; set; }
        public virtual int OwnerId { get; set; }
        public virtual CptObjectProperty Owner { get; set; }
        public BoolValues()
        {
            Owner = null!;
            Value = false;
        }
        public BoolValues(bool value)
        {
            Value = value;
            Owner = null!;
        }
    }
}
