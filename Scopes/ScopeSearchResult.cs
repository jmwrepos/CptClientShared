using CptClientShared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CptClientShared.Scopes
{
    public class ScopeSearchResult
    {
        public bool Found { get; set; }
        public List<LibraryScope> Libraries { get; set; }
        public List <ObjectScope> Objects { get; set; }
        public List<ObjectTypeScope> ObjectTypes { get; set; }
        public ResultId ResultId { get; set; }
        public static ScopeSearchResult LibNotFound => new() { ResultId = ResultId.LibNotFound };
        public static ScopeSearchResult ObjNotInLib => new() { ResultId = ResultId.ObjNotInLib };
        public static ScopeSearchResult ObjTypeNotFound => new() { ResultId = ResultId.ObjTypeNotFound };
        public ScopeSearchResult()
        {
            Libraries = new();
            Objects = new();
            ObjectTypes = new();
        }
    }
}
