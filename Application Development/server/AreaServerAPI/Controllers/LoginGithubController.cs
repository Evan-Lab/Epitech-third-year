using AreaServer.Core.Models;
using AreaServer.Infrastructure.Data.Repository;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using AreaServerAPI.Objects.Responses;

namespace AreaServerAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginGithubController : ControllerBase
    {
        private readonly ILogger<LoginGithubController> _logger;
        private IRepository<User> _userRepository;

        public LoginGithubController(IRepository<User> userRepository, ILogger<LoginGithubController> logger)
        {
            _logger = logger;
            _userRepository = userRepository;
        }

        [HttpGet("/github/link", Name = "GET_GITHUB_ACCESS")]
        [SwaggerOperation(
              Summary = "Login with Github",
              Description = "Get the link to login with Github",
              OperationId = "User.GetGithubAccess",
              Tags = new[] { "Github" })
        ]
        public async Task<ActionResult<List<string>>> LoginGithub()
        {
            string ClientId = "14166bc7624555604b6f";
            string RedirectUri = "http://localhost:8081/App-account/";
            var authorizationUrl = $"https://github.com/login/oauth/authorize?client_id={ClientId}&redirect_uri={RedirectUri}&scope=user%20repo";
            Console.WriteLine(authorizationUrl);
            return Ok(authorizationUrl);
        }
    }
}
