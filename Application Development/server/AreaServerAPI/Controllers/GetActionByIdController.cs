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
    public class ActionIdController : ControllerBase
    {
        public readonly ILogger<ActionIdController> _logger;
        public IRepository<ActionArea> _userRepository;

        public ActionIdController(IRepository<ActionArea> idActionRepository, ILogger<ActionIdController> logger)
        {
            _logger = logger;
            _userRepository = idActionRepository;
        }

        [HttpGet("/action/id", Name = "LOGIN_ACTION_ID")]
        [SwaggerOperation(
              Summary = "Get action information by id",
              Description = "Give the action information by id: idAction",
              OperationId = "Action.getActionById",
              Tags = new[] { "Action" })
        ]
        public async Task<ActionResult<List<string>>> GetActionById(IdActionRequest request)
        {
            var svc = new User()
            {
                Id = request.IdAction
            };

            var foundUser = await _userRepository.GetAsync(p => p.Id == request.IdAction);
            var response = new ApiResponse();

            if (foundUser == null) 
            {
                response.Response = "Action not found";
                return NotFound(response);
            }
            return Ok(foundUser);
        }
    }
}