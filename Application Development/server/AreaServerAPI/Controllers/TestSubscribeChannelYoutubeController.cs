using AreaServer.Core.Class;
using AreaServer.Core.Models;
using AreaServer.Infrastructure.Data.Repository;
using AreaServerAPI.Objects;
using AreaServerAPI.Objects.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.IO;

namespace AreaServerAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestSubscribeChannelYoutubeController : ControllerBase
    {
        public readonly ILogger<TestSubscribeChannelYoutubeController> _logger;
        public IConfiguration _configuration;
        public IRepository<User> _userRepository;
        public IRepository<UserSites> _userSitesRepository;

        public TestSubscribeChannelYoutubeController(IRepository<User> userRepository, IRepository<UserSites> userSitesRepository, IConfiguration configuration, ILogger<TestSubscribeChannelYoutubeController> logger)
        {
            _logger = logger;
            _userRepository = userRepository;
            _userSitesRepository = userSitesRepository;
            _configuration = configuration;
        }
        [Authorize]
        [HttpPost("/test/youtube/subscribe", Name = "POST_COMMENT_YOUTUBE")]
        [SwaggerOperation(
              Summary = "Test to subscribe channel",
              Description = "subscribe to channel youtube",
              OperationId = "UserArea.PostYoutubeComment",
              Tags = new[] { "Youtube" })
        ]
        public async Task<ActionResult<string>> PostComment()
        {
            var decryptTokenClass = new DecryptTokenUser(Request, _configuration, _userRepository);
            var decryptToken = await decryptTokenClass.GetInfoUserWithToken();
            if (decryptToken == null)
            {
                return NotFound("User not found");
            }

            Console.WriteLine(decryptToken.ToString());

            var getAccessToken = await _userSitesRepository.GetAsync(u => (u.User.Id == decryptToken.Id) && (u.Name == "Google"));
            if (getAccessToken == null)
            {
                return NotFound("Token Not found");
            }
            string urlToTest = "https://www.youtube.com/user/mYGotaga";

            Console.WriteLine(getAccessToken.UserKey);

            var reaction = new SubscribeChannelYoutube();

            bool post = await reaction.SubscribeChannel(urlToTest, getAccessToken.UserKey);
            if (post == false)
            {
                return BadRequest("Error Post comment");
            }
            return Ok("Comment posted");
        }
    }
}
