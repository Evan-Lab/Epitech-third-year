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
    public class GetAllServicesController : ControllerBase
    {
        private readonly ILogger<GetAllServicesController> _logger;
        private IRepository<Services> _servicesRepository;

        public GetAllServicesController(IRepository<Services> servicesRepository, ILogger<GetAllServicesController> logger)
        {
            _logger = logger;
            _servicesRepository = servicesRepository;
        }

        [HttpGet("/services/listServices", Name = "GET_ALL_AREA_SERVICES")]
        [SwaggerOperation(
              Summary = "Get the list of available services",
              Description = "Give the list of available services",
              OperationId = "Services.GetServicesList",
              Tags = new[] { "Services" })
        ]
        public async Task<ActionResult<List<string>>> GetServicesList()
        {
            var lst = await _servicesRepository.ListAsync();

            return Ok(lst);
        }

    }
}