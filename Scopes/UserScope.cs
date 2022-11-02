using CptClientShared.Entities.Accounting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CptClientShared.Scopes
{
    public class UserScope
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Street { get; set; } = string.Empty;
        public string Unit { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string Zip { get; set; } = string.Empty;
        public string PrimaryPhone { get; set; } = string.Empty;
        public string SecondaryPhone { get; set; } = string.Empty;
        public bool Active { get; set; }

        public UserScope(CptAcctUser user)
        {
            Id = user.Id;
            AccountId = user.AccountId;
            FirstName = user.FirstName;
            LastName = user.LastName;
            Email = user.Email;
            Street = user.Street;
            Unit = user.Unit;
            City = user.City;
            State = user.State;
            Zip = user.Zip;
            PrimaryPhone = user.PrimaryPhone;
            SecondaryPhone = user.SecondaryPhone;
            Active = user.Active;
        }
    }
}
