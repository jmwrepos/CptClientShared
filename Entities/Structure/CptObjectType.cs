using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CptClientShared.Entities.Structure
{
    public class CptObjectType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ParentLibraryId { get; set; }
        public virtual CptLibrary ParentLibrary { get; set; }
        public int? ParentId { get; set; }
        public virtual CptObjectType? ParentType { get; set; }
        public virtual List<CptObjectType> Children { get; set; }
        public virtual List<CptObject> Objects { get; set; }
        public virtual List<CptProperty> Properties { get; set; }
        public CptObjectType()
        {
            Name = string.Empty;
            ParentType = null!;
            ParentLibrary = null!;
            Children = new();
            Objects = new();
            Properties = new();
        }
        public CptObjectType(string name)
        {
            Name = name;
            ParentType = null!;
            ParentLibrary = null!;
            Children = new();
            Objects = new();
            Properties = new();
        }
        internal List<CptObjectType> DumpChildren(bool includeChildren = true)
        {
            List<CptObjectType> response = new();
            foreach (CptObjectType objType in Children)
            {
                response.Add(objType);
                if (includeChildren)
                {
                    response.AddRange(objType.DumpChildren());
                }
            }
            return response;
        }
        internal List<CptObjectType> DumpLineage(List<CptObjectType>? history = null)
        {
            List<CptObjectType> response = new();
            response.Add(this);
            if (ParentType != null)
            {
                response.AddRange(ParentType.DumpLineage(response));
            }
            if (Name == "Crimson")
            {
                Console.WriteLine("Index");
            }
            return response;
        }
    }
}
