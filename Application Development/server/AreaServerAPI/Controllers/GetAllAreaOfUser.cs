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
    public class GetAllAreaOfUser : ControllerBase
    {
        private readonly ILogger<GetAllAreaOfUser> _logger;
        private IConfiguration _configuration;
        private IRepository<User> _userRepository;
        private IRepository<UserArea> _userAreaRepository;

        public GetAllAreaOfUser(IRepository<User> userRepository, IRepository<UserArea> userAreaRepository, IConfiguration configuration, ILogger<GetAllAreaOfUser> logger)
        {
            _logger = logger;
            _userAreaRepository = userAreaRepository;
            _userRepository = userRepository;
            _configuration = configuration;
        }

        [HttpGet("/area/user/area", Name = "GET_ALL_AREA_OF_USER")]
        [SwaggerOperation(
              Summary = "Get the list of all area of user",
              Description = "Give the list of area of user",
              OperationId = "Area.GetAreaList",
              Tags = new[] { "Area" })
        ]
        public async Task<ActionResult<List<string>>> GetAreaOfUserList()
        {
            var lst = await _userAreaRepository.ListAsync();

            if (lst == null || !lst.Any())
            {
                return NotFound("No areas found");
            }

            var decryptTokenClass = new DecryptTokenUser(Request, _configuration, _userRepository);
            var decryptToken = await decryptTokenClass.GetInfoUserWithToken();
            if (decryptToken == null)
            {
                return NotFound("User not found");
            }

            var user = lst.Where(u => u.User != null && u.User.Id == decryptToken.Id).ToList();

            if (user == null || !user.Any())
            {
                return NotFound("No areas found for the user");
            }

            return Ok(user);
        }

    }
}