using CptClientShared;
using CptClientShared.Entities.Accounting;
using CptClientShared.QueryForms;
using CptClientShared.Services.ServiceEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CptClientShared.Services
{
    internal class SessionService
    {
        private readonly CptDbProvider _dbProvider;
        private Dictionary<string, CpSession> _sessions = new();
        public SessionService(CptDbProvider dbProvider)
        {
            _dbProvider = dbProvider;
        }
        public CptAuthResult AuthenticateUser(CptAcctUser user, QfAuthenticateUser form)
        {
            CptAuthResult result = new CptAuthResult();
            string unencrypted = ApiEncryption.Decrypt(user.Account.EncryptionKey, user.UserIV, user.Password);
            if (unencrypted == form.Password)
            {
                //SUCCESS
                result.Okay(true, user, "Success");
                _sessions[result.Session.Id] = result.Session;
            }
            else
            {
                //FAIL
                result.Fail();
            }
            return result;
        }
    }
}
