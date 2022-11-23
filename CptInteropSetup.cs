using CptClientShared.Entities.Accounting;
using CptClientShared.Entities.Structure;
using CptClientShared.QueryForms;

namespace CptClientShared
{
    internal class CptInteropSetup
    {
        private readonly CptDbProvider _dbProvider;
        public CptInteropSetup(CptDbProvider dbProvider)
        {
            _dbProvider = dbProvider;
        }
        public async Task<bool> DbExists()
        {
            try
            {
                CptAccount testobj1 = new();
                _dbProvider.CurrentContext.Add(testobj1);
                await _dbProvider.CurrentContext.SaveChangesAsync();
                _dbProvider.CurrentContext.Remove(testobj1);
                await _dbProvider.CurrentContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }

        }
        public async Task DbSetup(DbConfig2 config, QueryResponse qr)
        {
            try
            {
                config.Save();
                ConceptContext db = _dbProvider.NewContext();
                db.Database.EnsureCreated();
                db = _dbProvider.NewContext();
                List<CptAccount> accounts = db.Accounts.ToList();
                for(int i = 0; i < accounts.Count; i++)
                {
                    CptAccount acct = accounts[i];
                    db.Remove(acct);
                }
                await db.SaveChangesAsync();

                if (!db.Database.CanConnect())
                {
                    throw new InvalidOperationException($"Cannot connect to Db.");
                }

                qr.AddMessage("Ensuring default account exists.");
                bool defaultAcct = AcctExists(config.AccountName);
                if (!defaultAcct)
                {
                    qr.AddMessage("Adding Default Account.");
                    CptAccount defaultAccount = new(config);
                    qr.AddAccount(defaultAccount);
                    db.Accounts.Add(defaultAccount);
                    await SaveOnSuccess(qr, "ConceptDb Configured.");
                }
                db = _dbProvider.NewContext();
            }
            catch(Exception e)
            {
                FailOnException(qr, e);
            }
        }

        internal bool DefaultAccountExists()
        {
            DbConfig2 cfg = DbConfig2.Load();
            ConceptContext db = _dbProvider.CurrentContext;
            
            CptAccount? acct = db.Accounts
                .Where(a => a.AccountName == cfg.AccountName)
                .FirstOrDefault();

            if(acct != null && acct.Active == true && acct.Users.Count > 0)
            {
                return true;
            }
            return false;
        }

        private async Task SaveOnSuccess(QueryResponse qr, string msg)
        {
            try
            {
                await _dbProvider.CurrentContext.SaveChangesAsync();
                qr.Success = true;
                qr.AddMessage(msg);
            }
            catch (Exception e)
            {
                qr.Success = false;
                qr.AddMessage("Db Failure. Exception Follows.");
                qr.AddMessage(e.ToString());
            }
        }

        private void FailOnException(QueryResponse qr, Exception e)
        {
            qr.Success = false;
            qr.AddMessage("An application exception occured. Exception follows.");
            qr.AddMessage(e.ToString());
        }
        private void FailOnError(QueryResponse qr, string errMsg)
        {
            qr.Success = false;
            qr.AddMessage("An error occured. Details follow.");
            qr.AddMessage(errMsg);
        }
        private bool ObjectExists(CptLibrary library, string objName) => library.Objects.Any(o => o.Name == objName);
        private bool PropExists(CptLibrary lib, string name) => lib.Properties.Any(p => p.Name == name);
        private bool AcctExists(string name) => _dbProvider.CurrentContext.Accounts.Any(a => a.AccountName == name);
        private bool ObjTypeExists(CptLibrary lib, string name) => lib.ObjectTypes.Any(ot => ot.Name == name);
        private bool LibraryExists(CptAccount acct, string name) => acct.Libraries.Any(lib => lib.Name == name);
        private CptLibrary GetLibrary(CptAccount acct, string name) => acct.Libraries.Where(l => l.Name == name).First();
        private CptProperty GetProperty(CptLibrary lib, string name) => lib.Properties.Where(p => p.Name == name).First();
        private CptObject GetObject(CptLibrary lib, string name) => lib.Objects.Where(o => o.Name == name).First();
        private CptAccount GetAccount(string name) => _dbProvider.CurrentContext.Accounts.Where(a => a.AccountName == name).First();
    }
}
