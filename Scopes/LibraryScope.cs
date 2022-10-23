using CptClientShared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CptClientShared.Scopes
{
    public class LibraryScope
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<ObjectScope> Objects { get; set; }
        public List<ObjectTypeScope> Types { get; set; }
        public LibraryScope(CptLibrary library)
        {
            Name = library.Name;
            Id = library.Id;
            Objects = new();
            foreach (CptObject obj in library.Objects)
            {
                Objects.Add(new(obj));
            }
            Types = new();
            foreach (CptObjectType objType in library.ObjectTypes)
            {
                ObjectTypeScope objTypeScope = new(objType);
                Types.Add(objTypeScope);
            }
        }

        public LibraryScope()
        {
            Name = String.Empty;
            Objects = new();
            Types = new();
        }
        public List<ObjectScope> DumpAllObjects()
        {
            List<ObjectScope> result = new();
            foreach(ObjectScope obj in Objects)
            {
                result.Add(obj);
                result.AddRange(obj.DumpChildren());
            }
            return result;
        }

        public List<ObjectTypeScope> DumpAllTypes()
        {
            List<ObjectTypeScope> result = new();
            foreach (ObjectTypeScope objType in Types)
            {
                result.Add(objType);
            }
            return result;
        }
    }
}
