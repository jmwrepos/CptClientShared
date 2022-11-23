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
            StringValues = new();
            ObjNameValues = new();
            NumberValues = new();
            BytesValues = new();
            BoolValues = new();
        }
        public string? FirstStringValue(bool objName = false)
        {
            if (objName)
            {
                if (ObjNameValues.Count == 0)
                {
                    return null;
                }
                else
                {
                    return ObjNameValues[0].Value;
                }
            }
            else
            {
                if (StringValues.Count == 0)
                {
                    return null;
                }
                else
                {
                    return StringValues[0].Value;
                }
            }
        }

        public double? FirstNumberValue()
        {
            if (NumberValues.Count == 0)
            {
                return null;
            }
            else
            {
                return NumberValues[0].Value;
            }
        }

        public bool? FirstBoolValue()
        {
            if (BoolValues.Count == 0)
            {
                return null;
            }
            else
            {
                return BoolValues[0].Value;
            }
        }
        public byte[]? FirstBytesValue()
        {
            if (BytesValues.Count == 0)
            {
                return null;
            }
            else
            {
                return BytesValues[0].Value;
            }
        }
        public void SetSingleValue(string value, bool objName = false)
        {
            if (objName)
            {
                ObjNameValues = new() { new(value) };
            }
            else
            {
                StringValues = new() { new(value) };
            }
        }
        public void SetSingleValue(double value)
        {
            NumberValues = new() { new(value) };
        }
        public void SetSingleValue(int value)
        {
            NumberValues = new() { new(value) };
        }
        public void SetSingleValue(bool value)
        {
            BoolValues = new() { new(value) };
        }
        public void SetSingleValue(byte[] value)
        {
            BytesValues = new() { new(value) };
        }
    }
}
