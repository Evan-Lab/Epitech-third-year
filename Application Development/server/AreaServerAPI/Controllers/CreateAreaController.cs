using AreaServer.Core.Models;
using AreaServer.Infrastructure.Data.Repository;
using AreaServerAPI.Objects.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;
using System.Net.Mail;

namespace AreaServerAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class CreateAreaController : ControllerBase
    {
        public readonly ILogger<RegisterUserController> _logger;
        public IRepository<User> _userRepository;
        public IRepository<UserArea> _userAreaRepository;
        public IRepository<ActionArea> _actionAreaRepository;
        public IRepository<ReactionArea> _reactionAreaRepository;

        public CreateAreaController(IRepository<User> userRepository, IRepository<UserArea> userAreaRepository, IRepository<ActionArea> actionAreaRepository, IRepository<ReactionArea> reactionAreaRepository, ILogger<RegisterUserController> logger)
        {
            _logger = logger;
            _userRepository = userRepository;
            _userAreaRepository = userAreaRepository;
            _actionAreaRepository = actionAreaRepository;
            _reactionAreaRepository = reactionAreaRepository;
        }

        [HttpPost("/area/create", Name = "CREATE_AREA")]
        [SwaggerOperation(
              Summary = "Create a new request",
              Description = "Handle the creation of a new request: name, user id, action id, reaction id, parameters action, parameters reaction",
              OperationId = "UserAreas.CreateArea",
              Tags = new[] { "Area" })
        ]
        public async Task<ActionResult<string>> CreateArea(CreateAreaRequest request)
        {
            var user = await _userRepository.GetAsync(user => user.Id == request.UserId);

            if (user == null)
            {
                return NotFound("User not found");
            }

            var action = await _actionAreaRepository.GetAsync(action => action.Id == request.ActionId);

            if (action == null)
            {
                return NotFound("Action not found");
            }

            var reaction = await _reactionAreaRepository.GetAsync(reaction => reaction.Id == request.ReactionId);

            if (reaction == null)
            {
                return NotFound("Reaction not found");
            }

            var area = new UserArea()
            {
                Name = request.Name,
                User = user,
                ActionArea = action,
                ReactionArea = reaction,
                ParamAction = request.ParamAction,
                ParamReaction = request.ParamReaction,
                DateCreation = DateTime.UtcNow,
                IsActive = 1,
            };

            var result = await _userAreaRepository.AddAsync(area);
            return Ok(result);
        }
    }
}
