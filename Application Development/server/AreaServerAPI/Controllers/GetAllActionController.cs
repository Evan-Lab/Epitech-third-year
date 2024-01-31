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
    public class GetAllActionController : ControllerBase
    {
        private readonly ILogger<GetAllActionController> _logger;
        private IRepository<ActionArea> _actionRepository;

        public GetAllActionController(IRepository<ActionArea> actionRepository, ILogger<GetAllActionController> logger)
        {
            _logger = logger;
            _actionRepository = actionRepository;
        }

        [HttpGet("/action/listAction", Name = "GET_ALL_AREA_ACTION")]
        [SwaggerOperation(
              Summary = "Get the list of available Action",
              Description = "Give the list of available Action",
              OperationId = "Action.GetActionList",
              Tags = new[] { "Action" })
        ]
        public async Task<ActionResult<List<string>>> GetActionList()
        {
            var lst = await _actionRepository.ListAsync();

            return Ok(lst);
        }

    }
}