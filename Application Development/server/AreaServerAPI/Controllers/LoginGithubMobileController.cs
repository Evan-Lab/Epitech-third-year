using AreaServer.Core.Models;
using AreaServer.Infrastructure.Data.Repository;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using AreaServerAPI.Objects.Responses;

namespace AreaServerAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginGithubControllerMobile : ControllerBase
    {
        private readonly ILogger<LoginGithubControllerMobile> _logger;
        private IRepository<User> _userRepository;

        public LoginGithubControllerMobile(IRepository<User> userRepository, ILogger<LoginGithubControllerMobile> logger)
        {
            _logger = logger;
            _userRepository = userRepository;
        }

        [HttpGet("/githubMobile/link", Name = "GET_GITHUB_ACCESS_MOBILE")]
        [SwaggerOperation(
              Summary = "Login with Github",
              Description = "Get the link to login with Github",
              OperationId = "User.GetGithubAccessMobile",
              Tags = new[] { "Mobile" })
        ]
        public async Task<ActionResult<List<string>>> LoginGithubMobile()
        {
            string ClientId = "fc4af31f47cdb292db2d";
            string RedirectUri = "https://auth.expo.io/asuki/mobile";
            var authorizationUrl = $"https://github.com/login/oauth/authorize?client_id={ClientId}&redirect_uri={RedirectUri}&scope=user%20repo";
            Console.WriteLine(authorizationUrl);
            return Ok(authorizationUrl);
        }
    }
}