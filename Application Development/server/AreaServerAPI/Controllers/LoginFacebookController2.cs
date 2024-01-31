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
    public class LoginFacebookController2 : ControllerBase
    {
        private readonly ILogger<LoginFacebookController2> _logger;
        private IConfiguration _configuration;
        private IRepository<User> _userRepository;
        private IRepository<Services> _servicesRepository;
        private IRepository<UserSites> _userSitesRepository;

        public LoginFacebookController2(IRepository<User> userRepository, IRepository<Services> servicesRepository, IRepository<UserSites> userSitesRepository, IConfiguration configuration, ILogger<LoginFacebookController2> logger)
        {
            _logger = logger;
            _userRepository = userRepository;
            _servicesRepository = servicesRepository;
            _userSitesRepository = userSitesRepository;
            _configuration = configuration;
        }

        [HttpPost("/facebook/token", Name = "GET_FACEBOOK_ACCESS2")]
        [SwaggerOperation(
              Summary = "Login with Facebook",
              Description = "Login the user with Facebook and return: access_token",
              OperationId = "User.GetFacebookAccess2",
              Tags = new[] { "Facebook" })
        ]
        public async Task<ActionResult<List<string>>> LoginFacebook2(CodeFacebookRequest request)
        {
            try {
                var Response = new ApiResponse();
                string appId = "707658790781887";
                string appSecret = "c122b0dc0b895bada62e9aa6ec86db45";
                string authorizationCode = request.Code;

                using (HttpClient client = new HttpClient())
                {
                    var content = new FormUrlEncodedContent(new[]
                    {
                        new KeyValuePair<string, string>("client_id", appId),
                        new KeyValuePair<string, string>("client_secret", appSecret),
                        new KeyValuePair<string, string>("code", authorizationCode),
                        new KeyValuePair<string, string>("redirect_uri", "http://localhost:8081/App-account/")
                    });

                    HttpResponseMessage response = await client.PostAsync("https://graph.facebook.com/v18.0/oauth/access_token", content);

                    if (response.IsSuccessStatusCode) {
                        string json = await response.Content.ReadAsStringAsync();
                        dynamic result = Newtonsoft.Json.JsonConvert.DeserializeObject(json);
                        string accessToken = result.access_token;
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

                        var services = await _servicesRepository.GetAsync(s => s.Name == "Facebook");
                        if (services == null)
                        {
                            services = new Services()
                            {
                                Name = "Facebook",
                                DateCreation = DateTime.Now,
                            };
                            await _servicesRepository.AddAsync(services);
                        }

                        var foundUserSite = await _userSitesRepository.GetAsync(u => (u.User.Id == foundUser.Id) && (u.Name == "Facebook"));
                        if (foundUserSite == null)
                        {
                            var userSites = new UserSites()
                            {
                                Name = "Facebook",
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
                    } else {
                        Console.WriteLine("Error: " + response.StatusCode);
                        return NotFound();
                    }
                    Response.Response = "Connected !";
                    return Ok(Response);
                }
            } catch (Exception e) {
                Console.WriteLine(e.Message);
                return NotFound();
            }
        }
    }
}