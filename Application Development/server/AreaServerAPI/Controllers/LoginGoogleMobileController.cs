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
    public class LoginOAuthControllerMobile : ControllerBase
    {
        private readonly ILogger<LoginOAuthControllerMobile> _logger;
        private IRepository<User> _userRepository;

        public LoginOAuthControllerMobile(IRepository<User> userRepository, ILogger<LoginOAuthControllerMobile> logger)
        {
            _logger = logger;
            _userRepository = userRepository;
        }

        [HttpPost("/googleMobile/link", Name = "GET_GOOGLE_ACCESS_MOBILE")]
        [SwaggerOperation(
              Summary = "Login with Google",
              Description = "Get the link to login with Google: Give the page name (account or login)",
              OperationId = "User.GetGoogleAccessMobile",
              Tags = new[] { "Mobile" })
        ]
        public async Task<ActionResult<List<string>>> LoginGoogleMobile()
        {
            var Response = new ApiResponse();
            try {
                string clientId = "1088856570819-lpta18e63d9a6hh1pij901515etool3j.apps.googleusercontent.com";
                string redirectUri = "https://auth.expo.io/asuki/mobile";
                string scope = "openid email profile https://www.googleapis.com/auth/youtube.readonly https://www.googleapis.com/auth/photoslibrary.readonly";

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