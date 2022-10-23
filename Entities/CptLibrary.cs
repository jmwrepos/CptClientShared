using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CptClientShared.Entities
{
    public class CptLibrary
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public virtual List<CptObject> Objects { get; set; }
        public virtual List<CptObjectType> ObjectTypes { get; set; }
        public CptLibrary()
        {
            Name = string.Empty;
            Objects = new();
            ObjectTypes = new() { new("root") };
        }
    }
}
