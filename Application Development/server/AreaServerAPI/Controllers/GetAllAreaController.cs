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
    public class GetAllAreaController : ControllerBase
    {
        private readonly ILogger<GetAllAreaController> _logger;
        private IRepository<UserArea> _userAreaRepository;

        public GetAllAreaController(IRepository<UserArea> userAreaRepository, ILogger<GetAllAreaController> logger)
        {
            _logger = logger;
            _userAreaRepository = userAreaRepository;
        }

        [HttpGet("/area/listArea", Name = "GET_ALL_AREA_AREA")]
        [SwaggerOperation(
              Summary = "Get the list of all area",
              Description = "Give the list of available area",
              OperationId = "Area.GetAreaList",
              Tags = new[] { "Area" })
        ]
        public async Task<ActionResult<List<string>>> GetAreaList()
        {
            var lst = await _userAreaRepository.ListAsync();

            return Ok(lst);
        }

    }
}