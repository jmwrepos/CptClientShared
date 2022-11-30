using CptClientShared.Entities.Accounting;
using CptClientShared.Entities.Structure;
using CptClientShared.QueryForms;
using CptClientShared.Services;
using CptClientShared.Services.ServiceEntities;

namespace CptClientShared
{
    public enum CptStatus
    {
        CptError,
        CfgNotFound,
        DbConnectionError,
        DbError,
        CptReady
    }
    public class CptInterop
    {
        private readonly CptInteropSetup _iSetup;
        private readonly CptDbProvider _dbProvider;
        private readonly SessionService _sessionServ;
        public CptInterop()
        {
            _dbProvider = new();
            _iSetup = new(_dbProvider);
            _sessionServ = new(_dbProvider);
        }
        public async Task<CptStatus> Status()
        {
            string? connString = Environment.GetEnvironmentVariable("cpConnString");
            if(connString == null)
            {
                return CptStatus.CfgNotFound;
            }
            else
            {
                try
                {
                    if (!_dbProvider.CurrentContext.Database.CanConnect())
                    {
                        return CptStatus.DbConnectionError;
                    }

                    bool dbExists = await _iSetup.DbExists();
                    bool defAcctExists = await Task.Run(() => _iSetup.DefaultAccountExists());
                    if (!dbExists || !defAcctExists)
                    {
                        return CptStatus.DbError;
                    }
                    return CptStatus.CptReady;
                }
                catch
                {
                    return CptStatus.CptError;
                }
            }
        }

        public async Task UserSaveAsync()
        {
            await Task.Run(() => UserSave());
        }

        public async Task DbSetup(DbConfig2 configForm, QueryResponse qr) => await _iSetup.DbSetup(configForm, qr);
        public CpSession? Authenticate(string userId, string tryPassword)
        {
            ConceptContext db = _dbProvider.NewContext();
            List<CptAcctUser> acctUSers = db.AccountUsers.ToList();
            CptAcctUser? found = acctUSers.Where(au => au.Email.ToLower() == userId.ToLower()).FirstOrDefault();
            if (found != null)
            {
                //ENCRYPT AUTHENTICATION DATA
                QfAuthenticateUser form = new(userId, tryPassword);
                //AUTHENTICATE
                return  _sessionServ.AuthenticateUser(found, form).Session;
            }
            else
            {
                return null;
            }
        }

        public void HandleUserQuery(CptUserQuery uQuery)
        {
            QueryResponse qr = uQuery.Response;
            switch (uQuery.Operation)
            {
                case CpUQueryOperation.Unspecifed:
                    FailOnError(uQuery, "Operation Not Specified");
                    break;
                case CpUQueryOperation.ListLibraries:
                    foreach(CptLibrary lib in uQuery.User.Account.Libraries)
                    {
                        qr.AddListItem(lib.Name);
                        SaveOnSuccess(uQuery, "Libraries listed in Response.List");
                    }
                    break;
            }
        }

        //GENERAL METHODS
        private void SaveOnSuccess(CptUserQuery uQuery, string msg)
        {
            QueryResponse response = uQuery.Response;
            try
            {
                _dbProvider.CurrentContext.SaveChanges();
                response.Success = true;
                response.AddMessage(msg);
            }
            catch (Exception e)
            {
                FailOnException(uQuery, e);
            }
        }
        private void FailOnException(CptUserQuery uQuery, Exception e)
        {
            QueryResponse response = uQuery.Response;
            response.Success = false;
            response.AddMessage("An application exception occured. Exception follows.");
            response.AddMessage(e.ToString());
        }
        private void FailOnError(CptUserQuery uQuery, string errMsg)
        {
            QueryResponse response = uQuery.Response;
            response.Success = false;
            response.AddMessage("An error occured. Details follow.");
            response.AddMessage(errMsg);
        }

        public void AssertModel(UserModel model, QueryResponse qr)
        {
            try
            {
                CptAccount? acct = _dbProvider.CurrentContext.Accounts.Where(a => a.AccountName == model.AccountName).FirstOrDefault();
                if (acct != null)
                {
                    CptLibrary? lib = acct.Libraries.Where(l => l.Name == model.LibraryName).FirstOrDefault();
                    if (lib == null)
                    {
                        lib = new(model.LibraryName);
                        acct.Libraries.Add(lib);
                        qr.AddMessage($"Library Added {lib.Name}.");
                    }
                    foreach (CptTypeCfg typeCfg in model.UserTypes)
                    {
                        CptObjectType? objType = lib.ObjectTypes.Where(ot => ot.Name == typeCfg.Name).FirstOrDefault();
                        if (objType == null)
                        {
                            objType = new()
                            {
                                Name = typeCfg.Name,
                                ParentLibrary = lib,
                            };
                            lib.ObjectTypes.Add(objType);
                            qr.AddMessage($"Object Type Added {objType.Name}.");
                        }
                        foreach (string prop in typeCfg.Properties)
                        {
                            CptProperty? property = lib.Properties.Where(p => p.Name == prop).FirstOrDefault();
                            if (property == null)
                            {
                                property = new(prop);
                                lib.Properties.Add(property);
                                qr.AddMessage($"Property Added {property.Name}");
                            }
                            objType.Properties.Add(property);
                            property.ObjectTypes.Add(objType);
                        }
                    }
                    _dbProvider.CurrentContext.SaveChanges();
                    qr.AddMessage("Model Saved");
                    qr.Success = true;
                }
            }
            catch (Exception e)
            {
                qr.AddMessage($"Error: {e}");
                qr.Success = false;
            }
        }
        public void CreateNewObject(QfNewObj form)
        {
            CpSession? session = _sessionServ.GetSession(form.SessionId);
        }
        public void UserSave()
        {
            _dbProvider.CurrentContext.SaveChanges();
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