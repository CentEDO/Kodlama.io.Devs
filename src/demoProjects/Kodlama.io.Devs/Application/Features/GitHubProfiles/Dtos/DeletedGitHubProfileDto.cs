using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.GitHubProfiles.Dtos
{
    public class DeletedGitHubProfileDto
    {
        public int Id { get; set; }
        public string DeveloperEmail { get; set; }
        public string ProfileUrl { get; set; }
    }
}
