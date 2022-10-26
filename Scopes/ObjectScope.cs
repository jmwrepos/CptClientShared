using CptClientShared.Entities;

namespace CptClientShared.Scopes
{
    public class ObjectScope
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<string> ObjectTypes { get; set; }
        public List<ObjectScope> ChildObjects { get; set; }
        public List<ObjPropScope> ObjectProperties { get; set; }
        public ObjectScope(CptObject obj)
        {
            Id = obj.Id;
            Name = obj.Name;
            ChildObjects = new();
            ObjectTypes = new();
            ObjectProperties = new();
            foreach (CptObjectType objType in obj.ObjectTypes)
            {
                CptObjectType? otSlot = objType;
                while(otSlot != null)
                {
                    ObjectTypes.Add(otSlot.Name);
                    otSlot = otSlot.ParentType;
                }
            }
            foreach(CptObject child in obj.Children)
            {
                ChildObjects.Add(new(child));
            }
            foreach(CptObjectProperty objProp in obj.ObjectProperties)
            {
                ObjectProperties.Add(new(objProp));
            }
        }

        public ObjectScope()
        {
            Name = String.Empty;
            ChildObjects = new();
            ObjectTypes = new();
            ObjectProperties = new();
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