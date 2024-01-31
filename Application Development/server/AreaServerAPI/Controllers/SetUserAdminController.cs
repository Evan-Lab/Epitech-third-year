using AreaServer.Core.Models;
using AreaServer.Infrastructure.Data.Repository;
using AreaServerAPI.Objects;
using AreaServerAPI.Objects.Requests;
using AreaServerAPI.Objects.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.IO;

namespace AreaServerAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class SetUserAdmin : ControllerBase
    {
        public readonly ILogger<SetUserAdmin> _logger;
        private IConfiguration _configuration;
        private IRepository<User> _userRepository;

        public SetUserAdmin(IRepository<User> userRepository, IConfiguration configuration, ILogger<SetUserAdmin> logger)
        {
            _logger = logger;
            _userRepository = userRepository;
            _configuration = configuration;
        }

        [HttpPost("/user/set-admin", Name = "SET_ADMIN_USER")]
        [SwaggerOperation(
              Summary = "Set user's administrator role",
              Description = "Set user's administrator role",
              OperationId = "User.SetAdminUser",
              Tags = new[] { "User" })
        ]
        public async Task<ActionResult<string>> SetAdmin(IdUserRequest request)
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

            user.Admin = UserAdminStatus.IsModerator;
            await _userRepository.UpdateAsync(user);
            return Ok("The user's role has been successfully modified");
        }
    }
}
