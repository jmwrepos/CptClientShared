using CptClientShared.Entities.Accounting;
using CptClientShared.QueryForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CptClientShared.Services.ServiceEntities
{
    public class CptUserQuery
    {
        public CptAcctUser User { get; }
        public QueryResponse Response { get; set; }
        public CpUQueryOperation Operation { get; set; }
        public CptUserQuery(CptAcctUser user, CpUQueryOperation operation)
        {
            User = user;
            Response = new();
            Operation = operation;
        }
        public static CptUserQuery ListLibraries(CptAcctUser user) => new(user, CpUQueryOperation.ListLibraries);
    }
    public enum CpUQueryOperation
    {
        Unspecifed,
        ListLibraries
    }

}
