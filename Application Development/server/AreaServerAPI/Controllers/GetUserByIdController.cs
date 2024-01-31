using AreaServer.Core.Models;
using AreaServer.Infrastructure.Data.Repository;
using AreaServerAPI.Objects.Requests;
using AreaServerAPI.Objects.Responses;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Microsoft.AspNetCore.Authorization;

namespace AreaServerAPI.Controllers
{
    [Authorize]
    public class UserIdController : ControllerBase
    {
        public readonly ILogger<UserIdController> _logger;
        public IRepository<User> _userRepository;

        public UserIdController(IRepository<User> idUserRepository, ILogger<UserIdController> logger)
        {
            _logger = logger;
            _userRepository = idUserRepository;
        }

        [HttpGet("/user/id", Name = "LOGIN_USER_ID")]
        [SwaggerOperation(
              Summary = "Get user information by id",
              Description = "Give the user information by id: idUser",
              OperationId = "User.getUserById",
              Tags = new[] { "User" })
        ]
        public async Task<ActionResult<User>> GetUserById(IdUserRequest request)
        {
            var svc = new User()
            {
                Id = request.IdUser
            };

            var foundUser = await _userRepository.GetAsync(p => p.Id == request.IdUser);
            var response = new ApiResponse();

            if (foundUser == null) 
            {
                response.Response = "User not found";
                return NotFound(response);
            }
            return Ok(foundUser);
        }
    }
}