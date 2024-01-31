using AreaServer.Core.Models;
using AreaServer.Infrastructure.Data.Repository;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using AreaServerAPI.Objects.Responses;

namespace AreaServerAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginDeezerController : ControllerBase
    {
        private readonly ILogger<LoginDeezerController> _logger;
        private IRepository<User> _userRepository;

        public LoginDeezerController(IRepository<User> userRepository, ILogger<LoginDeezerController> logger)
        {
            _logger = logger;
            _userRepository = userRepository;
        }

        [HttpGet("/deezer/link", Name = "GET_DEEZER_ACCESS")]
        [SwaggerOperation(
              Summary = "Login with Deezer",
              Description = "Get the link to login with Deezer",
              OperationId = "User.GetDeezerAccess",
              Tags = new[] { "Deezer" })
        ]
        public async Task<ActionResult<List<string>>> LoginDeezer()
        {
            string appId = "640961";
            string RedirectUri = "http://localhost:8081/App-account/";
            var authorizationUrl = $"https://connect.deezer.com/oauth/auth.php?app_id={appId}&redirect_uri={RedirectUri}&perms=basic_access,email,manage_library,manage_community,delete_library,listening_history";
            Console.WriteLine(authorizationUrl);
            return Ok(authorizationUrl);
        }
    }
}
