using CptClientShared.Entities.Accounting;
using CptClientShared.Entities.Structure;
using Newtonsoft.Json;

namespace CptClientShared.Services.ServiceEntities
{
    public class CpSession
    {
        public string Id { get; set; } = Guid.Empty.ToString();
        private CptAcctUser? user { get; set; }
        [JsonIgnore]
        public CptLibrary? currentLib { get; set; }
        public DateTime Started { get; set; } = DateTime.UtcNow;
        public DateTime LastActivity { get; set; } = DateTime.UtcNow;
        public DateTime Expires { get; set; } = DateTime.UtcNow.AddDays(1);
        public void SetUser(CptAcctUser user)
        {
            Id = Guid.NewGuid().ToString();
            this.user = user;
            currentLib = user.Account.Libraries.FirstOrDefault();
        }
        [JsonIgnore]
        public CptAcctUser User => user!;
        public bool Ready => User != null;
        public static CpSession NotSet => new() { Id = "NotSet" };
    }
}
