using CptClientShared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CptClientShared.Scopes
{
    public class DbSearchResult
    {
        public bool Found { get; set; }
        public List<CptLibrary> Libraries { get; set; }
        public List <CptObject> Objects { get; set; }
        public List<CptObjectType> ObjectTypes { get; set; }
        public ResultId ResultId { get; set; }
        public DbSearchResult(bool found, ResultId resultId, List<CptLibrary> libraries = null!, List<CptObject> objects = null!)
        {
            Found = found;
            Libraries = libraries;
            Objects = objects;
            ResultId = resultId;
            ObjectTypes = null!;
        }
        public DbSearchResult(bool found, ResultId resultId, List<CptObjectType> objTypes = null!)
        {
            Found = found;
            Libraries = null!;
            Objects = null!;
            ResultId = resultId;
            ObjectTypes = objTypes;
        }
        public DbSearchResult(ResultId resultId)
        {
            Found = false;
            ResultId =resultId; 
            Libraries = null!;
            Objects = null!;
            ObjectTypes = null!;
        }
        public static DbSearchResult LibNotFound => new(ResultId.LibNotFound);
        public static DbSearchResult ObjNotInLib => new(ResultId.ObjNotInLib);
        public static DbSearchResult ObjTypeNotFound => new(ResultId.ObjTypeNotFound);
    }
    public enum ResultId
    {
        Unspecified,
        Success,
        LibNotFound,
        ObjNotInLib,
        ObjTypeNotFound
    }
}
