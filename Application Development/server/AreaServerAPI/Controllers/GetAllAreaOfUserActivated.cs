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
    public class GetAllAreaOfUserActivated : ControllerBase
    {
        private readonly ILogger<GetAllAreaOfUserActivated> _logger;
        private IConfiguration _configuration;
        private IRepository<User> _userRepository;
        private IRepository<UserArea> _userAreaRepository;

        public GetAllAreaOfUserActivated(IRepository<User> userRepository, IRepository<UserArea> userAreaRepository, IConfiguration configuration, ILogger<GetAllAreaOfUserActivated> logger)
        {
            _logger = logger;
            _userAreaRepository = userAreaRepository;
            _userRepository = userRepository;
            _configuration = configuration;
        }

        [HttpGet("/area/user/area-activated", Name = "GET_ALL_AREA_ACTIVATED_OF_USER")]
        [SwaggerOperation(
              Summary = "Get the list of all area activated of user",
              Description = "Give the list of area activated of user",
              OperationId = "Area.GetAreaActivatedList",
              Tags = new[] { "Area" })
        ]
        public async Task<ActionResult<List<string>>> GetAreaActivatedList()
        {
            var lst = await _userAreaRepository.ListAsync();

            var decryptTokenClass = new DecryptTokenUser(Request, _configuration, _userRepository);
            var decryptToken = await decryptTokenClass.GetInfoUserWithToken();
            if (decryptToken == null)
            {
                return NotFound("User not found");
            }

            return Ok(lst?.Where(u => u.User.Id == decryptToken.Id && u.IsActive == 1));
        }

    }
}