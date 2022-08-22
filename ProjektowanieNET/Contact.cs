using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace ProjektowanieNET
{
    public class Contact
    {
        public int? Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public DateTime Birthday { get; set; } = DateTime.UnixEpoch;


        public static bool Validate(Contact contact)
        {
            var validNrTelefonu = new Regex("^\\+?\\d{3,}").IsMatch(contact.Phone);

            return !(
                    string.IsNullOrEmpty(contact.FirstName) ||
                    string.IsNullOrEmpty(contact.Phone) ||
                    !validNrTelefonu ||
                    (!string.IsNullOrEmpty(contact.Email) && !new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$").IsMatch(contact.Email)) ||
                    contact.Birthday < DateTime.UnixEpoch
                );
        }

        public bool Matches(string s)
        {
            return s.Split(' ').Any(
                    t => (FirstName + LastName + Email + Phone + Birthday.ToString("d")).Contains(t)
                );
        }

        public bool MatchesExactly(string s)
        {
            return s.Equals(ToString());
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append(FirstName);
            sb.Append(" ");
            sb.Append(LastName);
            sb.Append(" " + Phone);
            sb.Append(" " + Email);
            sb.Append(" " + Birthday.ToString("D"));

            return sb.ToString();
        }

        public string Info => FirstName + " " + LastName + " has the birthday in " + Birthday.ToString("D");
    }
}
