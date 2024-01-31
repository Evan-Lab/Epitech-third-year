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
    public class ReactionIdController : ControllerBase
    {
        public readonly ILogger<ReactionIdController> _logger;
        public IRepository<ReactionArea> _userRepository;

        public ReactionIdController(IRepository<ReactionArea> idReactionRepository, ILogger<ReactionIdController> logger)
        {
            _logger = logger;
            _userRepository = idReactionRepository;
        }

        [HttpGet("/reaction/id", Name = "LOGIN_REACTION_ID")]
        [SwaggerOperation(
              Summary = "Get reaction information by id",
              Description = "Give the reaction information by id: idReaction",
              OperationId = "Reaction.getReactionById",
              Tags = new[] { "Reaction" })
        ]
        public async Task<ActionResult<List<string>>> GetReactionById(IdReactionRequest request)
        {
            var svc = new User()
            {
                Id = request.IdReaction
            };

            var foundUser = await _userRepository.GetAsync(p => p.Id == request.IdReaction);
            var response = new ApiResponse();

            if (foundUser == null) 
            {
                response.Response = "Reaction not found";
                return NotFound(response);
            }
            return Ok(foundUser);
        }
    }
}