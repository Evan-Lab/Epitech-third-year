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
    public class AreaUserIdController : ControllerBase
    {
        public readonly ILogger<AreaUserIdController> _logger;
        public IRepository<UserArea> _userAreaRepository;

        public AreaUserIdController(IRepository<UserArea> userAreaRepository, ILogger<AreaUserIdController> logger)
        {
            _logger = logger;
            _userAreaRepository = userAreaRepository;
        }

        [HttpGet("/area/areaUserId", Name = "LOGIN_AREA_USER_ID")]
        [SwaggerOperation(
              Summary = "Get area information by id",
              Description = "Give the area information by the user id: idAreaUser",
              OperationId = "Area.getAreaById",
              Tags = new[] { "Area" })
        ]
        public async Task<ActionResult<List<string>>> GetAreaById(IdAreaRequest request)
        {
            var svc = new User()
            {
                Id = request.IdAreaUser
            };

            var foundUser = await _userAreaRepository.GetAsync(p => p.User.Id == request.IdAreaUser);
            var response = new ApiResponse();

            if (foundUser == null) 
            {
                response.Response = "Area not found";
                return NotFound(response);
            }
            return Ok(foundUser);
        }
    }
}