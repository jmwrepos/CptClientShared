using CptClientShared.Entities.Vals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CptClientShared.Entities.Structure
{
    public class CptObjectProperty
    {
        public int Id { get; set; }
        public int PropertyId { get; set; }
        public virtual CptProperty Property { get; set; }
        internal string PropertyName => Property.Name;
        public int ObjectId { get; set; }
        public virtual CptObject Object { get; set; }
        internal string ObjectName => Object.Name;
        public virtual List<CptStringValue> StringValues { get; set; }
        public virtual List<CptObjectNameValue> ObjNameValues { get; set; }
        public virtual List<CptNumberValue> NumberValues { get; set; }
        public virtual List<CptBytesValue> BytesValues { get; set; }
        public virtual List<CptBoolValues> BoolValues { get; set; }
        public CptObjectProperty()
        {
            Property = null!;
            Object = null!;
            StringValues = new() { };
            ObjNameValues = new() { };
            NumberValues = new() { };
            BytesValues = new() { };
            BoolValues = new() { };
        }
        public string FirstStringValue()
        {
            CptStringValue? val = StringValues.FirstOrDefault();
            if(val != null)
            {
                return val.Value;
            }
            return string.Empty;
        }

        public double FirstNumberValue()
        {
            CptNumberValue? val = NumberValues.FirstOrDefault();
            if (val != null)
            {
                return val.Value;
            }
            return 0;
        }
        public byte[] FirstBytesValue()
        {

            CptBytesValue? val = BytesValues.FirstOrDefault();
            if (val != null)
            {
                return val.Value;
            }
            return Array.Empty<byte>();
        }
        public void SetSingleValue(string value)
        {
            CptStringValue? v = StringValues.FirstOrDefault();
            
            if(v == null)
            {
                v = new(value);
                v.Owner = this;
                StringValues.Add(v);
            }
            v.Value = value;

        }
        public void SetSingleValue(double value)
        {
            CptNumberValue? v = NumberValues.FirstOrDefault();
            if (v == null)
            {
                v = new(value);
                v.Owner = this;
                NumberValues.Add(v);
            }
            v.Value = value;

        }
        public void SetSingleValue(byte[] value)
        {
            CptBytesValue? v = BytesValues.FirstOrDefault();
            if (v == null)
            {
                v = new(value);
                v.Owner = this;
                BytesValues.Add(v);
            }
            v.Value = value;
        }
    }
}
