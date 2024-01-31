using AreaServer.Core.Models;
using AreaServer.Infrastructure.Data.Repository;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using AreaServerAPI.Objects.Responses;

namespace AreaServerAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginFacebookController : ControllerBase
    {
        private readonly ILogger<LoginFacebookController> _logger;
        private IRepository<User> _userRepository;

        public LoginFacebookController(IRepository<User> userRepository, ILogger<LoginFacebookController> logger)
        {
            _logger = logger;
            _userRepository = userRepository;
        }

        [HttpGet("/facebook/link", Name = "GET_FACEBOOK_ACCESS")]
        [SwaggerOperation(
              Summary = "Login with Facebook",
              Description = "Get the link to login with Facebook",
              OperationId = "User.GetFacebookAccess",
              Tags = new[] { "Facebook" })
        ]
        public async Task<ActionResult<List<string>>> LoginFacebook()
        {
            string appId = "707658790781887";
            string RedirectUri = "http://localhost:8081/App-account/";
            string facebookAuthorizationUrl = $"https://www.facebook.com/v18.0/dialog/oauth?client_id={appId}&redirect_uri={RedirectUri}&scope=email";
            Console.WriteLine(facebookAuthorizationUrl);
            return Ok(facebookAuthorizationUrl);
        }
    }
}