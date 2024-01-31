using AreaServer.Core.Models;
using AreaServer.Infrastructure.Data.Repository;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using AreaServerAPI.Objects.Responses;
using AreaServerAPI.Objects.Requests;
using AreaServerAPI.Objects;

namespace AreaServerAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginDeezerController2 : ControllerBase
    {
        private readonly ILogger<LoginDeezerController2> _logger;
        private IConfiguration _configuration;
        private IRepository<User> _userRepository;
        private IRepository<Services> _servicesRepository;
        private IRepository<UserSites> _userSitesRepository;

        public LoginDeezerController2(IRepository<User> userRepository, IRepository<Services> servicesRepository, IRepository<UserSites> userSitesRepository, IConfiguration configuration, ILogger<LoginDeezerController2> logger)
        {
            _logger = logger;
            _userRepository = userRepository;
            _servicesRepository = servicesRepository;
            _userSitesRepository = userSitesRepository;
            _configuration = configuration;
        }

        [HttpPost("/deezer/token", Name = "GET_DEEZER_ACCESS2")]
        [SwaggerOperation(
              Summary = "Login with Deezer",
              Description = "Login the user with Deezer and return: access_token",
              OperationId = "User.GetDeezerAccess2",
              Tags = new[] { "Deezer" })
        ]
        public async Task<ActionResult<List<string>>> LoginDeezer2([FromBody] CodeDeezerRequest request)
        {
            try
            {
                var Response = new ApiResponse();
                string appId = "640961";
                string appSecret = "691c1a618cf4acb442d3a89805a37920";
                string authorizationCode = request.Code;

                using (HttpClient client = new HttpClient())
                {
                    var content = new FormUrlEncodedContent(new[]
                    {
                        new KeyValuePair<string, string>("app_id", appId),
                        new KeyValuePair<string, string>("secret", appSecret),
                        new KeyValuePair<string, string>("code", authorizationCode)
                    });

                    HttpResponseMessage response = await client.PostAsync("https://connect.deezer.com/oauth/access_token.php", content);

                    if (response.IsSuccessStatusCode)
                    {
                        string json = await response.Content.ReadAsStringAsync();
                        Console.WriteLine(json);
                        // Parse the JSON to extract the access token.
                        string accessToken = json.Split('=')[1].Split('&')[0];
                        Console.WriteLine("Access Token: " + accessToken);
                        if (accessToken == null)
                        {
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

                        var services = await _servicesRepository.GetAsync(s => s.Name == "Deezer");
                        if (services == null)
                        {
                            services = new Services()
                            {
                                Name = "Deezer",
                                DateCreation = DateTime.Now,
                            };
                            await _servicesRepository.AddAsync(services);
                        }

                        var foundUserSite = await _userSitesRepository.GetAsync(u => (u.User.Id == foundUser.Id) && (u.Name == "Deezer"));
                        if (foundUserSite == null)
                        {
                            var userSites = new UserSites()
                            {
                                Name = "Deezer",
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

                    } else
                    {
                        Console.WriteLine("Error: " + response.StatusCode);
                        return NotFound();
                    }

                    Response.Response = "Connected !";
                    return Ok(Response);
                }
            } catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return NotFound();
            }
        }
    }
}