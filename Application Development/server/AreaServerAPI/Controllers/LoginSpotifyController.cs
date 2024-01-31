using AreaServer.Core.Models;
using AreaServer.Infrastructure.Data.Repository;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using AreaServerAPI.Objects.Responses;

namespace AreaServerAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginSpotifyController : ControllerBase
    {
        private readonly ILogger<LoginSpotifyController> _logger;
        private IRepository<User> _userRepository;

        public LoginSpotifyController(IRepository<User> userRepository, ILogger<LoginSpotifyController> logger)
        {
            _logger = logger;
            _userRepository = userRepository;
        }

        [HttpGet("/spotify/link", Name = "GET_SPOTIFY_ACCESS")]
        [SwaggerOperation(
              Summary = "Login with Spotify",
              Description = "Get the link to login with Spotify",
              OperationId = "User.GetSpotifyAccess",
              Tags = new[] { "Spotify" })
        ]
        public async Task<ActionResult<List<string>>> LoginSpotify()
        {
            string ClientId = "b413cd372c034577a94c6e24167020dd";
            string RedirectUri = "http://localhost:8081/App-account/";
            var scopes = "user-read-private user-read-email playlist-read-private user-follow-read user-follow-modify user-read-playback-state playlist-read-collaborative playlist-modify-private playlist-modify-public";
            var authorizationUrl = $"https://accounts.spotify.com/authorize" +
                $"?client_id={ClientId}" +
                $"&response_type=code" +
                $"&redirect_uri={Uri.EscapeDataString(RedirectUri)}" +
                $"&scope={Uri.EscapeDataString(scopes)}";
            Console.WriteLine(authorizationUrl);
            return Ok(authorizationUrl);
        }
    }
}
