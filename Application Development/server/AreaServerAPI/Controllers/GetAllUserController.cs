using AreaServer.Core.Models;
using AreaServer.Infrastructure.Data.Repository;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Microsoft.AspNetCore.Authorization;

namespace AreaServerAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class GetAllUserController : ControllerBase
    {
        private readonly ILogger<GetAllUserController> _logger;
        private IRepository<User> _userRepository;

        public GetAllUserController(IRepository<User> userRepository, ILogger<GetAllUserController> logger)
        {
            _logger = logger;
            _userRepository = userRepository;
        }

        [HttpGet("/user/listUser", Name = "GET_ALL_AREA_USER_LIST")]
        [SwaggerOperation(
              Summary = "Get the list of available user",
              Description = "Give the list of available user",
              OperationId = "User.GetUserList",
              Tags = new[] { "User" })
        ]
        public async Task<ActionResult<List<string>>> GetUserList()
        {
            var lst = await _userRepository.ListAsync();

            return Ok(lst);
        }
    }
}