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
    public class TestSpotifyReactionFollowArtistController : ControllerBase
    {
        public readonly ILogger<TestSpotifyReactionFollowArtistController> _logger;
        public IConfiguration _configuration;
        public IRepository<User> _userRepository;
        public IRepository<UserSites> _userSitesRepository;

        public TestSpotifyReactionFollowArtistController(IRepository<User> userRepository, IRepository<UserSites> userSitesRepository, IConfiguration configuration, ILogger<TestSpotifyReactionFollowArtistController> logger)
        {
            _logger = logger;
            _userRepository = userRepository;
            _userSitesRepository = userSitesRepository;
            _configuration = configuration;
        }
        [Authorize]
        [HttpPost("/test/spotify/follow-artist", Name = "FOLLOW_ARTIST_SPOTIFY")]
        [SwaggerOperation(
              Summary = "Test to follow artist",
              Description = "Test to follow artist",
              OperationId = "UserArea.FollowArtistSpotify",
              Tags = new[] { "Spotify" })
        ]
        public async Task<ActionResult<string>> FollowArtist(string urlArtist)
        {
            var decryptTokenClass = new DecryptTokenUser(Request, _configuration, _userRepository);
            var decryptToken = await decryptTokenClass.GetInfoUserWithToken();
            if (decryptToken == null)
            {
                return NotFound("User not found");
            }

            Console.WriteLine(decryptToken.ToString());

            var getAccessToken = await _userSitesRepository.GetAsync(u => (u.User.Id == decryptToken.Id) && (u.Name == "Spotify"));
            if (getAccessToken == null)
            {
                return NotFound("Token Not found");
            }

            var reaction = new SpotifyReaction(getAccessToken.UserKey);
            var doReaction = await reaction.FollowArtist(urlArtist);
            if (doReaction == false)
            {
                return BadRequest("Error");
            }
            return Ok("Artist followed");
        }
    }
}
