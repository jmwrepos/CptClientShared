using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CptClientShared.Entities.Structure
{
    public class CptObject
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public virtual List<CptObjectType> ObjectTypes { get; set; }
        public int LibraryId { get; set; }
        public virtual CptLibrary Library { get; set; }
        public int? ParentId { get; set; }
        public virtual CptObject? Parent { get; set; }
        public virtual List<CptObject> Children { get; set; }
        public virtual List<CptObjectProperty> ObjectProperties { get; set; }
        public CptObject()
        {
            ObjectTypes = new();
            Library = null!;
            Children = new();
            Name = string.Empty;
            Library = null!;
            Parent = null!;
            ObjectProperties = new();
        }
    }
}
