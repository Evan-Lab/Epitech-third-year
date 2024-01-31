using AreaServer.Core.Models;
using AreaServer.Infrastructure.Data.Repository;
using AreaServerAPI.Objects.Responses;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Security.Cryptography;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using AreaServerAPI.Objects;
using Microsoft.AspNetCore.Authorization;

namespace AreaServerAPI.Controllers
{
    [Authorize]
    public class GetIdUserWithToken : ControllerBase
    {
        public readonly ILogger<GetIdUserWithToken> _logger;
        public IRepository<User> _userRepository;
        private readonly IConfiguration _configuration;
        public GetIdUserWithToken(IRepository<User> userRepository, ILogger<GetIdUserWithToken> logger, IConfiguration configuration)
        {
            _logger = logger;
            _userRepository = userRepository;
            _configuration = configuration;
        }

        [HttpGet("/user/me", Name = "GET_ID_USER_WITH_TOKEN")]
        [SwaggerOperation(
              Summary = "Get user information by token",
              Description = "Give the user information by token: token",
              OperationId = "User.GetUserByToken",
              Tags = new[] { "User" })
        ]
        public async Task<ActionResult<User>> GetUserByToken()
        {
            var decryptTokenClass = new DecryptTokenUser(Request, _configuration, _userRepository);
            var decryptToken = await decryptTokenClass.GetInfoUserWithToken();
            if (decryptToken == null)
            {
                return NotFound("User not found");
            }

            Console.WriteLine(decryptToken);
            return Ok(decryptToken);
        }
    }
}
