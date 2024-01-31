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
    public class TestSpotifyReactionFollowPlaylist : ControllerBase
    {
        public readonly ILogger<TestSpotifyReactionFollowPlaylist> _logger;
        public IConfiguration _configuration;
        public IRepository<User> _userRepository;
        public IRepository<UserSites> _userSitesRepository;

        public TestSpotifyReactionFollowPlaylist(IRepository<User> userRepository, IRepository<UserSites> userSitesRepository, IConfiguration configuration, ILogger<TestSpotifyReactionFollowPlaylist> logger)
        {
            _logger = logger;
            _userRepository = userRepository;
            _userSitesRepository = userSitesRepository;
            _configuration = configuration;
        }
        [Authorize]
        [HttpPost("/test/spotify/follow-playlist", Name = "FOLLOW_PLAYLIST_SPOTIFY")]
        [SwaggerOperation(
              Summary = "Test to follow playlist",
              Description = "Test to follow playlist",
              OperationId = "UserArea.FollowPlaylistSpotify",
              Tags = new[] { "Spotify" })
        ]
        public async Task<ActionResult<string>> FollowPlaylist(string urlPlaylist)
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
            var doReaction = await reaction.FollowPlaylist(urlPlaylist);
            if (doReaction == false)
            {
                return BadRequest("Error");
            }
            return Ok("PLaylist followed");
        }
    }
}
