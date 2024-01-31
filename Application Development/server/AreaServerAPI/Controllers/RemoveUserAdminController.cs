using AreaServer.Core.Models;
using AreaServer.Infrastructure.Data.Repository;
using AreaServerAPI.Objects;
using AreaServerAPI.Objects.Requests;
using AreaServerAPI.Objects.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace AreaServerAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class RemoveUserAdmin : ControllerBase
    {
        public readonly ILogger<RemoveUserAdmin> _logger;
        private IConfiguration _configuration;
        private IRepository<User> _userRepository;

        public RemoveUserAdmin(IRepository<User> userRepository, IConfiguration configuration, ILogger<RemoveUserAdmin> logger)
        {
            _logger = logger;
            _userRepository = userRepository;
            _configuration = configuration;
        }

        [HttpPost("/user/remove-admin", Name = "REMOVE_ADMIN_USER")]
        [SwaggerOperation(
              Summary = "Remove user's administrator role",
              Description = "Remove user's administrator role",
              OperationId = "User.RemoveAdminUser",
              Tags = new[] { "User" })
        ]
        public async Task<ActionResult<string>> RemoveAdmin(IdUserRequest request)
        {
            var decryptTokenClass = new DecryptTokenUser(Request, _configuration, _userRepository);
            var decryptToken = await decryptTokenClass.GetInfoUserWithToken();
            if (decryptToken == null)
            {
                return NotFound("User not found");
            }
            if (decryptToken.Admin == UserAdminStatus.IsUser)
            {
                return Unauthorized("You are not authorize to do this request !");
            }

            var user = await _userRepository.GetAsync(u => u.Id == request.IdUser);
            if (user == null)
            {
                return NotFound("User not found");
            }

            user.Admin = UserAdminStatus.IsUser;
            await _userRepository.UpdateAsync(user);
            return Ok("The user's role has been successfully modified");
        }
    }
}
