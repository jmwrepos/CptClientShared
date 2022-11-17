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
        internal void Configure(DbConfig2 cptConfig)
        {
            Name = cptConfig.LibraryName;
            List<string> newProps = new();
            foreach(string prop in cptConfig.Properties)
            {
                Properties.Add(new(prop));
            }

            foreach(ObjTypeCfg2 subCfg in cptConfig.ObjectTypes)
            {
                CptObjectType? parent = ObjectTypes.Where(ot => ot.Name == subCfg.Name).FirstOrDefault();
                CptObjectType newType = new()
                {
                    Name = subCfg.Name,
                    ParentLibrary = this,
                    ParentType = parent,
                    Properties = GetPropList(subCfg.DefaultProperties).ToList()
                };
                foreach(CptProperty prop in newType.Properties)
                {
                    prop.ObjectTypes.Add(newType);
                }
            }
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
