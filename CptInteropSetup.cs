using CptClientShared;
using CptClientShared.Entities.Accounting;
using CptClientShared.Entities.Structure;
using CptClientShared.QueryForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CptClientShared
{
    internal class CptInteropSetup
    {
        private readonly CptDbProvider _dbProvider;
        public CptInteropSetup(CptDbProvider dbProvider)
        {
            _dbProvider = dbProvider;
        }
        public bool DbExists()
        {
            try
            {
                CptAccount testobj1 = new();
                _dbProvider.CurrentContext.Add(testobj1);
                _dbProvider.CurrentContext.SaveChanges();
                _dbProvider.CurrentContext.Remove(testobj1);
                _dbProvider.CurrentContext.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }

        }
        public void DbSetup(DbConfig2 config, QueryResponse qr)
        {
            try
            {
                ConceptContext db = _dbProvider.NewContext();
                if (config.EnsureDeleted)
                {
                    qr.AddMessage("Deleting Database.");
                    db.Database.EnsureDeleted();
                }
                if (!DbExists())
                {
                    qr.AddMessage("Ensuring Database Exists.");
                    db.Database.EnsureCreated();
                }
                qr.AddMessage("Ensuring default account exists.");
                bool defaultAcct = AcctExists(config.AccountName);
                if (!defaultAcct)
                {
                    qr.AddMessage("Adding Default Account.");
                    CptAccount defaultAccount = new();
                    qr.AddAccount(defaultAccount);
                    defaultAccount.Configure(config);
                    db.Accounts.Add(defaultAccount);
                }
                SaveOnSuccess(qr, "ConceptDb Configured.");
            }
            catch(Exception e)
            {
                FailOnException(qr, e);
            }
        }
        private void SaveOnSuccess(QueryResponse qr, string msg)
        {
            try
            {
                _dbProvider.CurrentContext.SaveChanges();
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
