using Application.Features.Developers.Commands.CreateDeveloper;
using Application.Features.Developers.Commands.LoginDeveloper;
using Application.Features.Developers.Dtos;
using Application.Features.Languages.Commands.CreateLanguage;
using Application.Features.Languages.Dtos;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeveloperController : BaseController
    {
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] CreateDeveloperCommand createDeveloperCommand)
        {
            TokenDto result = await Mediator.Send(createDeveloperCommand);
            return Created("", result);
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDeveloperCommand createDeveloperCommand)
        {
            TokenDto result = await Mediator.Send(createDeveloperCommand);
            return Created("", result);
        }
    }
}
