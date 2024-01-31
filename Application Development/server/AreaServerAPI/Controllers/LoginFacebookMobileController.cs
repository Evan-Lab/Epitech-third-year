using AreaServer.Core.Models;
using AreaServer.Infrastructure.Data.Repository;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using AreaServerAPI.Objects.Responses;

namespace AreaServerAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginFacebookControllerMobile : ControllerBase
    {
        private readonly ILogger<LoginFacebookControllerMobile> _logger;
        private IRepository<User> _userRepository;

        public LoginFacebookControllerMobile(IRepository<User> userRepository, ILogger<LoginFacebookControllerMobile> logger)
        {
            _logger = logger;
            _userRepository = userRepository;
        }

        [HttpGet("/facebookMobile/link", Name = "GET_FACEBOOK_ACCESS_MOBILE")]
        [SwaggerOperation(
              Summary = "Login with Facebook",
              Description = "Get the link to login with Facebook",
              OperationId = "User.GetFacebookAccessMobile",
              Tags = new[] { "Mobile" })
        ]
        public async Task<ActionResult<List<string>>> LoginFacebookMobile()
        {
            string appId = "707658790781887";
            string RedirectUri = "https://auth.expo.io/asuki/mobile";
            string facebookAuthorizationUrl = $"https://www.facebook.com/v18.0/dialog/oauth?client_id={appId}&redirect_uri={RedirectUri}&scope=email";
            Console.WriteLine(facebookAuthorizationUrl);
            return Ok(facebookAuthorizationUrl);
        }
    }
}