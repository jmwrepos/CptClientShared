using CptClientShared.Entities.Accounting;
using CptClientShared.Entities.Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CptClientShared.QueryForms
{
    public class QueryResponse
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public bool Success { get; set; } = true;
        public DbConfig2? ConfigUsed { get; set; }
        public List<string> Messages { get; set; } = new();
        public List<CptAccount> Accounts { get; set; } = new();
        public List<CptLibrary> Libraries { get; set; } = new();
        public List<CptAcctUser> Users { get; set; } = new();
        public List<CptObject> Objects { get; set; } = new();
        public List<CptObjectType> ObjectTypes { get; set; } = new();
        public List<CptProperty> Properties { get; set; } = new();
        public List<CptObjectProperty> ObjProperties { get; set; } = new();
        public List<string> List { get; set; } = new();
        public void AddMessage(string msg) => Messages.Add(msg);
        public bool AddAccount(CptAccount account)
        {
            if (!Accounts.Contains(account))
            {
                Accounts.Add(account);
                return true;
            }
            return false;
        }
        public bool AddListItem(string item)
        {
            if (List.Contains(item))
            {
                return false;
            }
            List.Add(item);
            return true;
        }
        public bool AddUser(CptAcctUser user)
        {
            bool found = false;
            foreach(CptAccount acct in Accounts)
            {
                if (acct.Users.Contains(user))
                {
                    found = true;
                    break;
                }
            }
            if (Users.Contains(user))
            {
                found = true;
            }
            if (!found)
            {
                Users.Add(user);
                return true;
            }
            return false;
        }
        public bool AddObject(CptObject obj)
        {
            bool found = false;
            foreach(CptAccount acct in Accounts)
            {
                foreach(CptLibrary lib in acct.Libraries)
                {
                    if (lib.Objects.Contains(obj))
                    {
                        found = true;
                        break;
                    }
                }
                if (found)
                {
                    break;
                }
            }
            foreach(CptLibrary lib in Libraries)
            {
                if (lib.Objects.Contains(obj))
                {
                    found = true;
                    break;
                }
            }
            if (Objects.Contains(obj))
            {
                found = true;
            }
            if (!found)
            {
                Objects.Add(obj);
                return true;
            }
            return false;
        }
        public bool AddObjectType(CptObjectType objType)
        {
            bool found = false;
            foreach(CptAccount acct in Accounts)
            {
                foreach(CptLibrary lib in acct.Libraries)
                {
                    if (lib.ObjectTypes.Contains(objType))
                    {
                        found = true;
                        break;
                    }
                }
                if (found)
                {
                    break;
                }
            }
            foreach(CptLibrary lib in Libraries)
            {
                if (lib.ObjectTypes.Contains(objType))
                {
                    found = true;
                    break;
                }
            }
            if (ObjectTypes.Contains(objType))
            {
                found = true;
            }
            if (!found)
            {
                ObjectTypes.Add(objType);
                return true;
            }
            return false;
        }
        public bool AddProperty(CptProperty prop)
        {
            bool found = false;
            foreach(CptAccount acct in Accounts)
            {
                foreach(CptLibrary lib in acct.Libraries)
                {
                    if (lib.Properties.Contains(prop))
                    {
                        found = true;
                        break;
                    }
                }
                if (found)
                {
                    break;
                }
            }
            foreach(CptLibrary lib in Libraries)
            {
                if (lib.Properties.Contains(prop))
                {
                    found = true;
                }
                break;
            }
            if (Properties.Contains(prop))
            {
                found = true;
            }
            if (!found)
            {
                Properties.Add(prop);
                return true;
            }
            return false;
        }
        public bool AddObjProperty(CptObjectProperty objProp)
        {
            bool found = false;
            foreach(CptAccount acct in Accounts)
            {
                foreach(CptLibrary lib in acct.Libraries)
                {
                    foreach(CptObject obj in lib.Objects)
                    {
                        if (obj.ObjectProperties.Contains(objProp))
                        {
                            found = true;
                            break;
                        }
                    }
                    if (found)
                    {
                        break;
                    }
                    foreach (CptProperty prop in lib.Properties)
                    {
                        if (prop.ObjectProperties.Contains(objProp))
                        {
                            found = true;
                            break;
                        }
                    }
                    if (found)
                    {
                        break;
                    }
                }
                if (found)
                {
                    break;
                }
            }

            foreach (CptLibrary lib in Libraries)
            {
                foreach (CptObject obj in lib.Objects)
                {
                    if (obj.ObjectProperties.Contains(objProp))
                    {
                        found = true;
                        break;
                    }
                }
                if (found)
                {
                    break;
                }

                foreach(CptProperty prop in lib.Properties)
                {
                    if (prop.ObjectProperties.Contains(objProp))
                    {
                        found = true;
                        break;
                    }
                }
                if (found)
                {
                    break;
                }
            }
            foreach (CptObject obj in Objects)
            {
                if (obj.ObjectProperties.Contains(objProp))
                {
                    found = true;
                    break;
                }
            }
            foreach (CptProperty prop in Properties)
            {
                if (prop.ObjectProperties.Contains(objProp))
                {
                    found = true;
                    break;
                }
            }
            if (ObjProperties.Contains(objProp))
            {
                found = true;
            }
            if (!found)
            {
                ObjProperties.Add(objProp);
                return true;
            }
            return false;
        }
        public bool AddLibrary(CptLibrary library)
        {
            bool found = false;
            foreach(CptAccount acct in Accounts)
            {
                if (acct.Libraries.Contains(library))
                {
                    found = true;
                    break;
                }
            }
            if (Libraries.Contains(library))
            {
                found = true;
            }
            if (!found)
            {
                Libraries.Add(library);
                return true;
            }
            return false;
        }

    }
}
