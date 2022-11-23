using CptClientShared.Entities.Accounting;
using CptClientShared.QueryForms;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CptClientShared.Entities.Structure
{
    public class CptLibrary
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public virtual List<CptObject> Objects { get; set; }
        public virtual List<CptObjectType> ObjectTypes { get; set; }
        public virtual List<CptProperty> Properties { get; set; }
        public int AccountId { get; set; }
        public virtual CptAccount Account { get; set; }
        public CptLibrary()
        {
            Name = string.Empty;
            Objects = new();
            ObjectTypes = new();
            Properties = new();
            Account = null!;
        }
        public CptLibrary(string name)
        {
            Name = name;
            Objects = new();
            ObjectTypes = new();
            Properties = new();
            Account = null!;
        }

        private IEnumerable<CptProperty> GetPropList(List<string> defaultProperties)
        {
            foreach(string prop in defaultProperties)
            {
                yield return Properties.Where(p => p.Name == prop).First();
            }
        }
    }
}
