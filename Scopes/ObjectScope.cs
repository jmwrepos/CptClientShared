using CptClientShared.Entities;

namespace CptClientShared.Scopes
{
    public class ObjectScope
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<string> ObjectTypes { get; set; }
        public List<ObjectScope> ChildObjects { get; set; }
        public ObjectScope(CptObject obj)
        {
            Id = obj.Id;
            Name = obj.Name;
            ChildObjects = new();
            ObjectTypes = new();            
            foreach(CptObjectType objType in obj.ObjectTypes)
            {
                foreach(CptObjectType listed in objType.DumpLineage())
                {
                    if (!ObjectTypes.Contains(listed.Name))
                    {
                        ObjectTypes.Add(listed.Name);
                    }
                }
            }
            foreach(CptObject child in obj.Children)
            {
                ChildObjects.Add(new(child));
            }
        }

        public ObjectScope()
        {
            Name = String.Empty;
            ChildObjects = new();
            ObjectTypes = new();
        }

        internal List<ObjectScope> DumpChildren(bool includeChildren = true)
        {
            List<ObjectScope> response = new();
            foreach(ObjectScope obj in ChildObjects)
            {
                response.Add(obj);
                if (includeChildren)
                {
                    response.AddRange(obj.DumpChildren());
                }
            }
            return response;
        }
    }
}