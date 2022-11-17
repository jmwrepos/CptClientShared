using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CptClientShared.QueryForms
{
    public class DbConfig2
    {
        [JsonIgnore]
        public string ToJson => JsonConvert.SerializeObject(this, Formatting.Indented);
        public void Save() { File.WriteAllText("./cptCfg.json", ToJson); }
        public static DbConfig2 Load => JsonConvert.DeserializeObject<DbConfig2>(File.ReadAllText("./cptCfg.json")) ?? throw new InvalidDataException("Config Not Found");
        public bool EnsureDeleted { get; set; }
        public string ConnectionString { get; set; }
        public string AccountName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string LibraryName { get; set; }
        public List<ObjTypeCfg2> ObjectTypes { get; set; }
        public List<string> Properties { get; set; } = new();

        public DbConfig2()
        {
            ConnectionString = string.Empty;
            AccountName = string.Empty;
            FirstName = string.Empty;
            LastName = string.Empty;
            Email = string.Empty;
            Password = string.Empty;
            LibraryName = string.Empty;
            ObjectTypes = new();
        }
        public void SetDb(string connString)
        {
            ConnectionString = connString;
        }
        public void DeleteOnStart(bool val) { EnsureDeleted = val; }
        public void SetDefaultAccount(string acctName, string username, string password, string firstName, string lastName, string libName)
        {
            AccountName = acctName;
            Email = username;
            Password = password;
            FirstName = firstName;
            LastName = lastName;
            LibraryName = libName;
        }
        public void AddObjectType(string name, List<string> properties, string? parent = null)
        {
            bool exist = ObjectTypes.Any(ot => ot.Name == name);
            if (!exist)
            {
                ObjTypeCfg2 newObjType = new(name, properties, parent);
                foreach(string property in properties)
                {
                    if (!Properties.Contains(property))
                    {
                        Properties.Add(property);
                    }
                }
            }
        }
    }
}
