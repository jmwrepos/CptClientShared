using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CptClientShared.Entities
{
    public class CptProperty
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual List<CptObjectProperty> ObjectProperties { get; set; }
        public int LibraryId { get; set; }
        public virtual CptLibrary Library { get; set; }
        public CptProperty()
        {
            Name = String.Empty;
            Library = null!;
            ObjectProperties = new();
        }
    }
}
