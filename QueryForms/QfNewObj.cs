using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CptClientShared.QueryForms
{
    public class QfNewObj
    {
        public string SessionId { get; set; } = string.Empty;
        public string LibraryName { get; set; } = string.Empty;
        public string ObjectType { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string? Parent { get; set; } = null;
        public List<QfNewObjProp> ObjProps { get; set; } = new();
    }
}
