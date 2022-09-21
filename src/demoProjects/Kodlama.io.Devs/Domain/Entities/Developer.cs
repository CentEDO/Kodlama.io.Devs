using Core.Security.Entities;
using Core.Security.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Developer:User
    {
        public virtual ICollection<GitHubProfile> GitHubProfiles { get; set; }

        public Developer()
        {

        }

        public Developer(int id, string firstName, string lastName, string email, byte[] passwordSalt, byte[] passwordHash, bool status, AuthenticatorType authenticatorType, int gitHubProfileId) : base(id, firstName, lastName, email, passwordSalt, passwordHash, status, authenticatorType)
        {

        }
    }
}
