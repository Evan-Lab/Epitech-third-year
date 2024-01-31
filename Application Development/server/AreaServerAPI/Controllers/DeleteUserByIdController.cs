using AreaServer.Core.Models;
using AreaServer.Infrastructure.Data.Repository;
using AreaServerAPI.Objects.Requests;
using AreaServerAPI.Objects.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace AreaServerAPI.Controllers
{
    [Authorize]
    public class DeleteUserId : ControllerBase
    {
        public readonly ILogger<DeleteUserId> _logger;
        public IRepository<User> _userRepository;
        public DeleteUserId(IRepository<User> userRepository, ILogger<DeleteUserId> logger)
        {
            _logger = logger;
            _userRepository = userRepository;
        }
        [HttpGet("/user/deleteUserID", Name = "DELETE_USER_ID")]
        [SwaggerOperation(
              Summary = "Delete user by id",
              Description = "Delete the user with his id: idUser",
              OperationId = "User.deleteUserId",
              Tags = new[] { "User" })
        ]
        public async Task<ActionResult<List<string>>> DelUserId(DeleteUserIdRequest request)
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
            int userId = request.IdUser;
            await _userRepository.DeleteAsync(foundUser);
            return Ok(foundUser);
        }
    }
}