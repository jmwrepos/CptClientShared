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
            StringValues = new() { new(string.Empty) };
            ObjNameValues = new() { new(string.Empty)};
            NumberValues = new() { new(0) };
            BytesValues = new() { new(Array.Empty<byte>()) };
            BoolValues = new() { new(false) };
        }
        public string FirstStringValue(bool objName = false)
        {
            if (objName)
            {
                 return ObjNameValues[0].Value;
            }
            else
            {
                return StringValues[0].Value;
            }
        }

        public double FirstNumberValue()
        {
            return NumberValues[0].Value;
        }

        public bool FirstBoolValue()
        {
            return BoolValues[0].Value;
        }
        public byte[] FirstBytesValue()
        {
            return BytesValues[0].Value;
        }
        public void SetSingleValue(string value, bool objName = false)
        {
            if (objName)
            {
                ObjNameValues[0].Value = value;
            }
            else
            {
                StringValues[0].Value = value;
            }
        }
        public void SetSingleValue(double value)
        {
            NumberValues[0].Value = value;
        }
        public void SetSingleValue(int value)
        {
            NumberValues[0].Value = value;
        }
        public void SetSingleValue(bool value)
        {
            BoolValues[0].Value = value; ;
        }
        public void SetSingleValue(byte[] value)
        {
            BytesValues[0].Value = value; ;
        }
    }
}
