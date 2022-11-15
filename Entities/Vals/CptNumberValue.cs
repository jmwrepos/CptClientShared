using CptClientShared.Entities.Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CptClientShared.Entities.Vals
{
    public class CptNumberValue
    {
        public int Id { get; set; }
        public double Value { get; set; }
        public virtual int OwnerId { get; set; }
        public virtual CptObjectProperty Owner { get; set; }
        public CptNumberValue()
        {
            Owner = null!;
        }
        public CptNumberValue(double value)
        {
            Value = value;
            Owner = null!;
        }
    }
}
