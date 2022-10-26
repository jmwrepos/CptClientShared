using CptClientShared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CptClientShared.Scopes
{
    public class ObjPropScope
    {
        public int Id { get; set; }
        public string Property { get; set; }
        public List<string> StringValues { get; set; }
        public List<double> NumberValues { get; set; }
        public List<string> ObjNameValues { get; set; }
        public ObjPropScope(CptObjectProperty objProp)
        {
            Id = objProp.Id;
            Property = objProp.PropertyName;
            StringValues = new();
            NumberValues = new();
            ObjNameValues = new();
        }
        public ObjPropScope()
        {
            Property = String.Empty;
            StringValues = new();
            NumberValues = new();
            ObjNameValues = new();
        }
    }
}
