using AreaServer.Core.Models;
using AreaServer.Infrastructure.Data.Repository;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using AreaServerAPI.Objects.Responses;
using AreaServerAPI.Objects.Requests;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using AreaServerAPI.Objects;
using Microsoft.Extensions.Configuration;

namespace AreaServerAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginSpotifyController2 : ControllerBase
    {
        private readonly ILogger<LoginSpotifyController2> _logger;
        private IConfiguration _configuration;
        private IRepository<User> _userRepository;
        private IRepository<UserSites> _userSitesRepository;
        private IRepository<Services> _servicesRepository;

        public LoginSpotifyController2(IRepository<User> userRepository, IRepository<Services> servicesRepository, IRepository<UserSites> userSitesRepository, IConfiguration configuration, ILogger<LoginSpotifyController2> logger)
        {
            _logger = logger;
            _userRepository = userRepository;
            _servicesRepository = servicesRepository;
            _userSitesRepository = userSitesRepository;
            _configuration = configuration;
        }

        [HttpPost("/spotify/token", Name = "GET_SPOTIFY_ACCESS2")]
        [SwaggerOperation(
              Summary = "Login with Spotify",
              Description = "Login the user with Spotify and return: access_token",
              OperationId = "User.GetSpotifyAccess2",
              Tags = new[] { "Spotify" })
        ]
        public async Task<ActionResult<List<string>>> LoginSpotify2([FromBody] CodeSpotifyRequest request)
        {
            try {
                var Response = new ApiResponse();
                string ClientId = "b413cd372c034577a94c6e24167020dd";
                string RedirectUri = "http://localhost:8081/App-account/";
                string ClientSecret = "8e5023c24928472984d0401003788f02";
                var requestUri = "https://accounts.spotify.com/api/token";
                var code = request.Code;
                var accessToken = "";

                var httpClient = new HttpClient();
                var requestContent = new FormUrlEncodedContent(new Dictionary<string, string>
                {
                    { "client_id", ClientId },
                    { "client_secret", ClientSecret },
                    { "code", code },
                    { "redirect_uri", RedirectUri },
                    { "grant_type", "authorization_code" } // Indiquez le type de subvention
                });
                var response = await httpClient.PostAsync(requestUri, requestContent);
                var responseContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine("Content: " + responseContent);
                var jsonObject = JObject.Parse(responseContent);
                accessToken = jsonObject["access_token"].ToString();
                Console.WriteLine("Access Token: " + accessToken);
                if (accessToken == null) {
                    Response.Response = "Error: Access Token is null";
                    return NotFound(Response);
                }

                var decryptTokenClass = new DecryptTokenUser(Request, _configuration, _userRepository);
                var decryptToken = await decryptTokenClass.GetInfoUserWithToken();
                if (decryptToken == null)
                {
                    return NotFound("User not found");
                }

                Console.WriteLine(decryptToken);

                var foundUser = await _userRepository.GetAsync(u => u.Id == decryptToken.Id);
                if (foundUser == null)
                {
                    return NotFound("User not found");
                }

                var services = await _servicesRepository.GetAsync(s => s.Name == "Spotify");
                if (services == null)
                {
                    services = new Services()
                    {
                        Name = "Spotify",
                        DateCreation = DateTime.Now,
                    };
                    await _servicesRepository.AddAsync(services);
                }

                var foundUserSite = await _userSitesRepository.GetAsync(u => (u.User.Id == foundUser.Id) && (u.Name == "Spotify"));
                if (foundUserSite == null)
                {
                    var userSites = new UserSites()
                    {
                        Name = "Spotify",
                        User = foundUser,
                        DateCreation = DateTime.Now,
                        UserKey = accessToken,
                        Services = services,
                        Email = foundUser.Email,
                    };

                    await _userSitesRepository.AddAsync(userSites);
                }
                else
                {
                    foundUserSite.UserKey = accessToken;

                    await _userSitesRepository.UpdateAsync(foundUserSite);
                }

                Response.Response = "Connected !";
                return Ok(Response);
            } catch (Exception e) {
                Console.WriteLine(e.Message);
                return NotFound();
            }
        }
    }
}