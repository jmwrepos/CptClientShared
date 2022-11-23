using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CptClientShared.QueryForms
{
    public class CptTypeCfg
    {
        public string Name { get; set; }
        public string? Parent { get; set; }
        public List<string> Properties { get; set; }
        public CptTypeCfg(string name, CptTypeCfg? parent, List<string> properties)
        {
            Name = name;
            Parent = parent?.Name;
            Properties = properties;
        }
    }
}
