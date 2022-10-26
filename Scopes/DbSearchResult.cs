using CptClientShared.Entities;

namespace CptClientShared.Scopes
{
    public class DbSearchResult
    {
        public bool Found { get; set; }
        public List<CptLibrary> Libraries { get; set; }
        public List <CptObject> Objects { get; set; }
        public List<CptObjectType> ObjectTypes { get; set; }
        public List<CptProperty> Properties { get; set; }
        public ResultId ResultId { get; set; }
        public DbSearchResult(bool found, ResultId resultId, List<CptLibrary> libraries = null!, List<CptObject> objects = null!)
        {
            Found = found;
            Libraries = libraries;
            Objects = objects;
            ResultId = resultId;
            ObjectTypes = null!;
            Properties = null!;
        }
        public DbSearchResult(bool found, ResultId resultId, List<CptObjectType> objTypes = null!)
        {
            Found = found;
            Libraries = null!;
            Objects = null!;
            ResultId = resultId;
            ObjectTypes = objTypes;
            Properties = null!;
        }

        public static DbSearchResult PropFound(CptProperty found)
        {
            return new(ResultId.Success)
            {
                Properties = new() { found },
                Found = true
            };
        }
        public DbSearchResult(ResultId resultId)
        {
            Found = false;
            ResultId =resultId; 
            Libraries = null!;
            Objects = null!;
            ObjectTypes = null!;
            Properties = null!;
        }
        public static DbSearchResult LibNotFound => new(ResultId.LibNotFound);
        public static DbSearchResult ObjNotInLib => new(ResultId.ObjNotInLib);
        public static DbSearchResult ObjTypeNotFound => new(ResultId.ObjTypeNotFound);
        public static DbSearchResult PropNotFound => new(ResultId.PropNotFound);
    }
    public enum ResultId
    {
        Unspecified,
        Success,
        LibNotFound,
        ObjNotInLib,
        ObjTypeNotFound,
        PropNotFound
    }
}
