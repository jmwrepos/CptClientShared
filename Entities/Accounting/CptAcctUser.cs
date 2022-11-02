using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CptClientShared.Entities.Accounting
{
    public class CptAcctUser
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        [Required]
        public virtual CptAccount Account { get; set; } = null!;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public byte[] Password { get; set; } = Array.Empty<byte>();
        public string Street { get; set; } = string.Empty;
        public string Unit { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string Zip { get; set; } = string.Empty;
        public string PrimaryPhone { get; set; } = string.Empty;
        public string SecondaryPhone { get; set; } = string.Empty;
        public bool Active { get; set; }
        public byte[] UserIV { get; set; } = Array.Empty<byte>();
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            Regex emailReg = new(@"(\w+@[a-zA-Z_]+?\.[a-zA-Z]{2,6})");
            Regex streetReg = new(@"^\s*\S+(?:\s+\S+){2}");
            Regex nameReg = new(@"[a-zA-Z]+");
            Regex stateReg = new(@"^(([Aa][EeLlKkSsZzRr])|([Cc][AaOoTt])|([Dd][EeCc])|([Ff][MmLl])|([Gg][AaUu])|([Hh][Ii])|([Ii][DdLlNnAa])|([Kk][SsYy])|([Ll][Aa])|([Mm][EeHhDdAaIiNnSsOoTt])|([Nn][EeVvHhJjMmYyCcDd])|([Mm][Pp])|([Oo][HhKkRr])|([Pp][WwAaRr])|([Rr][Ii])|([Ss][CcDd])|([Tt][NnXx])|([Uu][Tt])|([Vv][TtIiAa])|([Ww][AaVvIiYy]))$");
            Regex zipReg = new(@"^\d{5}(?:[-\s]\d{4})?$");
            Regex phoneReg = new(@"/\(?([0-9]{3})\)?([ .-]?)([0-9]{3})\2([0-9]{4})/");

            bool firstNameValid = nameReg.IsMatch(FirstName);
            bool lastNameValid = nameReg.IsMatch(LastName);
            bool emailValid = emailReg.IsMatch(Email);
            bool streetValid = streetReg.IsMatch(Street);
            bool cityValid = nameReg.IsMatch(City);
            bool stateValid = stateReg.IsMatch(State);
            bool zipValid = zipReg.IsMatch(Zip);
            bool phone1Valid = phoneReg.IsMatch(PrimaryPhone);
            bool phone2Valid = phoneReg.IsMatch(SecondaryPhone);

            List<bool> checks = new() { firstNameValid, lastNameValid, emailValid, streetValid, cityValid, stateValid, zipValid, phone1Valid || PrimaryPhone == String.Empty, phone2Valid || SecondaryPhone == String.Empty };

            for (int i = 0; i < checks.Count; i++)
            {
                if (!checks[i])
                {
                   yield return new ValidationResult(
                   ValidationMessage(i),
                   new[] { ValidationElement(i) });
                }
            }
        }

        private static string ValidationElement(int i)
        {
            return i switch
            {
                0 => "Invalid First Name",
                1 => "Invalid Last Name",
                2 => "Invalid Email",
                3 => "Invalid Street",
                4 => "Invalid City",
                5 => "Invalid Zip",
                6 => "Invalid Primary Phone",
                7 => "Invalid Secondary Phone",
                _ => throw new InvalidOperationException("Invalid Option"),
            };
        }

        private string ValidationMessage(int i)
        {
            return i switch
            {
                0 => nameof(FirstName),
                1 => nameof(LastName),
                2 => nameof(Email),
                3 => nameof(Street),
                4 => nameof(City),
                5 => nameof(Zip),
                6 => nameof(PrimaryPhone),
                7 => nameof(SecondaryPhone),
                _ => throw new InvalidOperationException("Invalid Option"),
            };
        }
    }
}
