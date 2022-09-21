using Application.Features.Developers.Commands.CreateDeveloper;
using Application.Features.Developers.Dtos;
using Application.Features.GitHubProfiles.Commands.CreateGitHubProfile;
using Application.Features.GitHubProfiles.Commands.DeleteGitHubProfile;
using Application.Features.GitHubProfiles.Commands.UpdateGitHubProfile;
using Application.Features.GitHubProfiles.Dtos;
using Application.Features.Languages.Commands.CreateLanguage;
using Application.Features.Languages.Commands.DeleteLanguage;
using Application.Features.Languages.Commands.UpdateLanguage;
using Application.Features.Languages.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GitHubProfileController : BaseController
    {
        [HttpPost("connect")]
        public async Task<IActionResult> Create([FromBody] CreateGitHubProfileCommand createdGitHubProfileCommand)
        {
            CreatedGitHubProfileDto result = await Mediator.Send(createdGitHubProfileCommand);
            return Created("", result);
        }
        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete([FromRoute] DeleteGitHubProfileCommand deleteGitHubProfileCommand)
        {
            DeletedGitHubProfileDto result = await Mediator.Send(deleteGitHubProfileCommand);
            return Ok(result);
        }
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateGitHubProfileCommand updateGitHubProfileCommand)
        {
            UpdatedGitHubProfileDto result = await Mediator.Send(updateGitHubProfileCommand);
            return Ok(result);
        }
    }
}
