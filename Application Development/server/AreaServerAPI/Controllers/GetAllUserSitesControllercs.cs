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
    public class GetAllUserSitesController : ControllerBase
    {
        private readonly ILogger<GetAllUserSitesController> _logger;
        private IRepository<UserSites> _userSitesRepository;

        public GetAllUserSitesController(IRepository<UserSites> userSitesRepository, ILogger<GetAllUserSitesController> logger)
        {
            _logger = logger;
            _userSitesRepository = userSitesRepository;
        }

        [HttpGet("/user-sites/listUserSites", Name = "GET_ALL_USER_SITES_LIST")]
        [SwaggerOperation(
              Summary = "Get the list of all sites of user",
              Description = "Get the list of all sites of user",
              OperationId = "UserSites.GetUserSitesList",
              Tags = new[] { "UserSites" })
        ]
        public async Task<ActionResult<List<string>>> GetUserList()
        {
            var lst = await _userSitesRepository.ListAsync();

            return Ok(lst);
        }
    }
}
