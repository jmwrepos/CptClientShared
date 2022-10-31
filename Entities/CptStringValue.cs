﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CptClientShared.Entities
{
    public class CptStringValue
    {
        public int Id { get; set; }
        public string Value { get; set; }
        public virtual int OwnerId { get; set; }
        public virtual CptObjectProperty Owner { get; set; }
        public CptStringValue()
        {
            Value = String.Empty;
            Owner = null!;
        }
        public CptStringValue(string value)
        {
            Value = value;
            Owner = null!;
        }
    }
}
