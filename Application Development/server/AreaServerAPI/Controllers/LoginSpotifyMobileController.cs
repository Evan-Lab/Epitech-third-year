using AreaServer.Core.Models;
using AreaServer.Infrastructure.Data.Repository;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using AreaServerAPI.Objects.Responses;

namespace AreaServerAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginSpotifyControllerMobile : ControllerBase
    {
        private readonly ILogger<LoginSpotifyControllerMobile> _logger;
        private IRepository<User> _userRepository;

        public LoginSpotifyControllerMobile(IRepository<User> userRepository, ILogger<LoginSpotifyControllerMobile> logger)
        {
            _logger = logger;
            _userRepository = userRepository;
        }

        [HttpGet("/spotifyMobile/link", Name = "GET_SPOTIFY_ACCESSf_MOBILE")]
        [SwaggerOperation(
              Summary = "Login with Spotify",
              Description = "Get the link to login with Spotify",
              OperationId = "User.GetSpotifyAccessMobile",
              Tags = new[] { "Mobile" })
        ]
        public async Task<ActionResult<List<string>>> LoginSpotifyMobile()
        {
            string ClientId = "6baa6bb0124346c7b6d4e881849dcbfc";
            string RedirectUri = "https://auth.expo.io/asuki/mobile";
            var scopes = "user-read-private user-read-email playlist-read-private user-follow-read user-read-playback-state playlist-read-collaborative";
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