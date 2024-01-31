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
    public class TestSpotifyReactionAddTrackInPlaylistController : ControllerBase
    {
        public readonly ILogger<TestSpotifyReactionAddTrackInPlaylistController> _logger;
        public IConfiguration _configuration;
        public IRepository<User> _userRepository;
        public IRepository<UserSites> _userSitesRepository;

        public TestSpotifyReactionAddTrackInPlaylistController(IRepository<User> userRepository, IRepository<UserSites> userSitesRepository, IConfiguration configuration, ILogger<TestSpotifyReactionAddTrackInPlaylistController> logger)
        {
            _logger = logger;
            _userRepository = userRepository;
            _userSitesRepository = userSitesRepository;
            _configuration = configuration;
        }
        [Authorize]
        [HttpPost("/test/spotify/add-track-playlist", Name = "ADD_TRACK_IN_PLAYLIST_SPOTIFY")]
        [SwaggerOperation(
              Summary = "Test to add track in playlist",
              Description = "Test to add track in playlist",
              OperationId = "UserArea.AddTrackInPlaylistSpotify",
              Tags = new[] { "Spotify" })
        ]
        public async Task<ActionResult<string>> FollowArtist(string urlPlaylist, string urlTrack)
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
            var doReaction = await reaction.AddTrackInPlaylist(urlPlaylist, urlTrack);
            if (doReaction == false)
            {
                return BadRequest("Error");
            }
            return Ok("Music added");
        }
    }
}

