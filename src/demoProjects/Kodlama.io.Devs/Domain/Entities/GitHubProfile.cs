using Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class GitHubProfile : Entity
    {
        public int DeveloperId { get; set; }
        public string ProfileUrl { get; set; }
        public virtual Developer Developer { get; set; }

        public GitHubProfile()
        {

        }

        public GitHubProfile(int developerId, string profileUrl) : this()
            => (DeveloperId, ProfileUrl) = (developerId, profileUrl);
    }
}
