using AreaServer.Core.Models;
using AreaServer.Infrastructure.Data.Repository;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using AreaServerAPI.Objects.Responses;
using AreaServerAPI.Objects.Requests;

namespace AreaServerAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginOAuthController : ControllerBase
    {
        private readonly ILogger<LoginOAuthController> _logger;
        private IRepository<User> _userRepository;

        public LoginOAuthController(IRepository<User> userRepository, ILogger<LoginOAuthController> logger)
        {
            _logger = logger;
            _userRepository = userRepository;
        }

        [HttpPost("/google/link", Name = "GET_GOOGLE_ACCESS")]
        [SwaggerOperation(
              Summary = "Login with Google",
              Description = "Get the link to login with Google: Give the page name (account or login)",
              OperationId = "User.GetGoogleAccess",
              Tags = new[] { "Google" })
        ]
        public async Task<ActionResult<List<string>>> LoginGoogle(UriRedirectRequest request)
        {
            var Response = new ApiResponse();
            try {
                string clientId = "1088856570819-48shcg2abgv52mrgt6e0453t8ep70pnq.apps.googleusercontent.com";
                string redirectUri = "http://localhost:8081/";
                string scope = "openid email profile https://www.googleapis.com/auth/youtube.readonly https://www.googleapis.com/auth/youtube.force-ssl";

                if (request.PageName == "account")
                    redirectUri += "App-account/";
                else if (request.PageName == "login")
                    redirectUri += "google/";
                else
                    return NotFound("Page not found: " + request.PageName);

                string authorizationUrl = $"https://accounts.google.com/o/oauth2/auth?client_id={clientId}&redirect_uri={redirectUri}&scope={scope}&response_type=code";
                Console.WriteLine(authorizationUrl);
                return Ok(authorizationUrl);
            } catch (Exception e) {
                Console.WriteLine(e.Message);
                Response.Response = "Non";
                return NotFound(Response);
            }
        }
    }
}
