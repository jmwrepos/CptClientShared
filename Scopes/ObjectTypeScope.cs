using CptClientShared.Entities;

namespace CptClientShared.Scopes
{
    public class ObjectTypeScope
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<ObjectTypeScope> Subtypes { get; set; }
        public ObjectTypeScope(CptObjectType objType)
        {
            Id = objType.Id;
            Name = objType.Name;
            Subtypes = new();
            foreach(CptObjectType subType in objType.Children)
            {
                ObjectTypeScope objTypeScope = new(subType);
                Subtypes.Add(objTypeScope);
            }
        }
        public ObjectTypeScope()
        {
            Name = string.Empty;
            Subtypes = new();
        }
    }
}