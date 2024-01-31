using AreaServer.Core.Models;
using AreaServer.Infrastructure.Data.Repository;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Microsoft.AspNetCore.Authorization;

namespace AreaServerAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class GetAllReactionController : ControllerBase
    {
        private readonly ILogger<GetAllReactionController> _logger;
        private IRepository<ReactionArea> _reactionRepository;

        public GetAllReactionController(IRepository<ReactionArea> reactionRepository, ILogger<GetAllReactionController> logger)
        {
            _logger = logger;
            _reactionRepository = reactionRepository;
        }

        [HttpGet("/reaction/listReaction", Name = "GET_ALL_AREA_REACTION")]
        [SwaggerOperation(
              Summary = "Get the list of available Reaction",
              Description = "Give the list of available Reaction",
              OperationId = "Reaction.GetReactionList",
              Tags = new[] { "Reaction" })
        ]
        public async Task<ActionResult<List<string>>> GetReactionList()
        {
            var lst = await _reactionRepository.ListAsync();

            return Ok(lst);
        }

    }
}