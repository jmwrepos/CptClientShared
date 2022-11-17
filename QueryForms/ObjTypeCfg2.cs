using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CptClientShared.QueryForms
{
    public class ObjTypeCfg2
    {
        public string Name { get; } = string.Empty;
        public string? Parent { get; } = string.Empty;
        public List<string> DefaultProperties { get; } = new List<string>();
        public ObjTypeCfg2(string name, List<string> properties, string? parent = null)
        {
            Name = name;
            Parent = parent;
            DefaultProperties = properties;
        }
        public ObjTypeCfg2()
        {
        }
    }
}
