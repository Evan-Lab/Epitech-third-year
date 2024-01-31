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
    public class LoginGithubController2 : ControllerBase
    {
        private readonly ILogger<LoginGithubController2> _logger;
        private IConfiguration _configuration;
        private IRepository<User> _userRepository;
        private IRepository<Services> _servicesRepository;
        private IRepository<UserSites> _userSitesRepository;

        public LoginGithubController2(IRepository<User> userRepository, IRepository<Services> servicesRepository, IRepository<UserSites> userSitesRepository, IConfiguration configuration, ILogger<LoginGithubController2> logger)
        {
            _logger = logger;
            _userRepository = userRepository;
            _servicesRepository = servicesRepository;
            _userSitesRepository = userSitesRepository;
            _configuration = configuration;
        }

        [HttpPost("/github/token", Name = "GET_GITHUB_ACCESS2")]
        [SwaggerOperation(
              Summary = "Login with Github",
              Description = "Login the user with Github and return: access_token",
              OperationId = "User.GetGithubAccess2",
              Tags = new[] { "Github" })
        ]
        public async Task<ActionResult<List<string>>> LoginGithub2([FromBody] CodeGithubRequest request)
        {
            try {
                var Response = new ApiResponse();
                string ClientId = "14166bc7624555604b6f";
                string RedirectUri = "http://localhost:8081/App-account/";
                string ClientSecret = "e0c34cb6c40dc4544c9869c651aa40e077933667";
                var requestUri = "https://github.com/login/oauth/access_token";
                var code = request.Code;
                var accessToken = "";

                var httpClient = new HttpClient();
                var requestContent = new FormUrlEncodedContent(new Dictionary<string, string>
                {
                    { "client_id", ClientId },
                    { "client_secret", ClientSecret },
                    { "code", code },
                    { "redirect_uri", RedirectUri }
                });

                var response = await httpClient.PostAsync(requestUri, requestContent);
                var responseContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine("Content: " + responseContent);
                accessToken = responseContent.Split('=')[1].Split('&')[0];
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

                var services = await _servicesRepository.GetAsync(s => s.Name == "Github");
                if (services == null)
                {
                    services = new Services()
                    {
                        Name = "Github",
                        DateCreation = DateTime.Now,
                    };
                    await _servicesRepository.AddAsync(services);
                }

                var foundUserSite = await _userSitesRepository.GetAsync(u => (u.User.Id == foundUser.Id) && (u.Name == "Github"));
                if (foundUserSite == null)
                {
                    var userSites = new UserSites()
                    {
                        Name = "Github",
                        User = foundUser,
                        DateCreation = DateTime.Now,
                        UserKey = accessToken,
                        Services = services,
                        Email = foundUser.Email,
                    };

                    await _userSitesRepository.AddAsync(userSites);
                } else
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