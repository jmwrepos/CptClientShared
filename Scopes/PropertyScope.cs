using CptClientShared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CptClientShared.Scopes
{
    public class PropertyScope
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public PropertyScope(CptProperty obj)
        {
            Id = obj.Id;
            Name = obj.Name;
        }
        public PropertyScope()
        {
            Name = String.Empty;
        }
    }
}
