using CptClientShared.Entities.Accounting;
using CptClientShared.Entities.Structure;
using Newtonsoft.Json;

namespace CptClientShared.Services.ServiceEntities
{
    public class CpSession
    {
        public string Id { get; set; } = Guid.Empty.ToString();
        private CptAcctUser? user { get; set; }
        public DateTime Started { get; set; } = DateTime.Now;
        public DateTime LastActivity { get; set; } = DateTime.Now;
        public DateTime Expires { get; set; } = DateTime.Now.AddMinutes(15);
        public void SetUser(CptAcctUser user)
        {
            Id = Guid.NewGuid().ToString();
            this.user = user;
        }
        [JsonIgnore]
        public CptAcctUser User => user!;
        public bool Ready => User != null;
    }
}
