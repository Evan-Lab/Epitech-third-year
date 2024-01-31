using AreaServer.Core.Models;
using AreaServer.Infrastructure.Data.Repository;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Microsoft.AspNetCore.Authorization;
using AreaServerAPI.Objects;

namespace AreaServerAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class GetAllAreaOfUserDesactivated : ControllerBase
    {
        private readonly ILogger<GetAllAreaOfUserDesactivated> _logger;
        private IConfiguration _configuration;
        private IRepository<User> _userRepository;
        private IRepository<UserArea> _userAreaRepository;

        public GetAllAreaOfUserDesactivated(IRepository<User> userRepository, IRepository<UserArea> userAreaRepository, IConfiguration configuration, ILogger<GetAllAreaOfUserDesactivated> logger)
        {
            _logger = logger;
            _userAreaRepository = userAreaRepository;
            _userRepository = userRepository;
            _configuration = configuration;
        }

        [HttpGet("/area/user/area-desactivated", Name = "GET_ALL_AREA_DESACTIVATED_OF_USER")]
        [SwaggerOperation(
              Summary = "Get the list of all area desactivated of user",
              Description = "Give the list of area desactivated of user",
              OperationId = "Area.GetAreaDesactivatedList",
              Tags = new[] { "Area" })
        ]
        public async Task<ActionResult<List<string>>> GetAreaDesactivatedList()
        {
            var lst = await _userAreaRepository.ListAsync();

            var decryptTokenClass = new DecryptTokenUser(Request, _configuration, _userRepository);
            var decryptToken = await decryptTokenClass.GetInfoUserWithToken();
            if (decryptToken == null)
            {
                return NotFound("User not found");
            }

            return Ok(lst?.Where(u => u.User.Id == decryptToken.Id && u.IsActive == 0));
        }

    }
}