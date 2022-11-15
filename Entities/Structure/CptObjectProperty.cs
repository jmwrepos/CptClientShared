﻿using CptClientShared.Entities.Vals;
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
        public virtual List<BoolValues> BoolValues { get; set; }
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
    }
}