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
        public void Save() { File.WriteAllText("./cptCfg.json", JsonConvert.SerializeObject(this, Formatting.Indented)); }
        public static DbConfig2 Load() {
            try
            {
                string file = File.ReadAllText("./cptCfg.json");
                return JsonConvert.DeserializeObject<DbConfig2>(file) ?? new();
            }
            catch {
                return new();
            }

        }
        public string AccountName { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string License { get; set; } = string.Empty;
    }
}
