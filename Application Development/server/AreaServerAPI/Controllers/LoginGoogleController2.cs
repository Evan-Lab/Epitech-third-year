using AreaServer.Core.Models;
using AreaServer.Infrastructure.Data.Repository;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using AreaServerAPI.Objects.Responses;
using AreaServerAPI.Objects.Requests;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AreaServerAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginOAuthController2 : ControllerBase
    {
        private readonly ILogger<LoginOAuthController2> _logger;
        private IRepository<User> _userRepository;
        private IRepository<Services> _servicesRepository;
        private IRepository<UserSites> _userSitesRepository;
        private readonly TokenService _tokenService;
        private readonly IConfiguration _configuration;

        public LoginOAuthController2(IRepository<User> userRepository, IRepository<Services> servicesRepository, IRepository<UserSites> userSitesRepository, ILogger<LoginOAuthController2> logger, IConfiguration configuration)
        {
            _logger = logger;
            _userRepository = userRepository;
            _tokenService = new TokenService(configuration);
            _configuration = configuration;
            _servicesRepository = servicesRepository;
            _userSitesRepository = userSitesRepository;
        }

        [HttpPost("/google/token", Name = "GET_GOOGLE_ACCESS2")]
        [SwaggerOperation(
              Summary = "Login with Google",
              Description = "Login the user with Google and return user information",
              OperationId = "User.GetGoogleAccess2",
              Tags = new[] { "Google" })
        ]
        public async Task<ActionResult<List<string>>> LoginGoogle2([FromBody] CodeGoogleRequest request)
        {
            var Response = new ApiResponse();
            var code = request.Code;
            var pageName = request.PageName;
            try {
                string clientId = "1088856570819-48shcg2abgv52mrgt6e0453t8ep70pnq.apps.googleusercontent.com";
                string clientSecret = "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";
                string redirectUri = "http://localhost:8081/";
                string tokenUrl = "https://oauth2.googleapis.com/token";

                if (pageName == "account")
                    redirectUri += "App-account/";
                else if (pageName == "login")
                    redirectUri += "google/";
                else
                    return NotFound("Page not found: " + pageName);
                var postData = new List<KeyValuePair<string, string>> {
                    new KeyValuePair<string, string>("client_id", clientId),
                    new KeyValuePair<string, string>("client_secret", clientSecret),
                    new KeyValuePair<string, string>("code", code),
                    new KeyValuePair<string, string>("grant_type", "authorization_code"),
                    new KeyValuePair<string, string>("redirect_uri", redirectUri)
                };
                Console.WriteLine("Url: " + redirectUri);
                var httpClient = new HttpClient();
                var content = new FormUrlEncodedContent(postData);
                var response1 = await httpClient.PostAsync(tokenUrl, content);
                var requestContent = await response1.RequestMessage.Content.ReadAsStringAsync();
                Console.WriteLine("Request: " + requestContent);
                if (response1.IsSuccessStatusCode == false) {
                    Console.WriteLine("Error: status code 1ere request" + response1.StatusCode);
                    return NotFound("Error while getting token");
                }

                string responseString = await response1.Content.ReadAsStringAsync();
                var tokenData = JsonConvert.DeserializeObject<Dictionary<string, string>>(responseString);
                Console.WriteLine("Token: " + tokenData["access_token"]);
                if (pageName == "login") {
                    Console.WriteLine("Login...");
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenData["access_token"]);
                    var response2 = await httpClient.GetAsync("https://www.googleapis.com/oauth2/v2/userinfo");
                    if (response2.IsSuccessStatusCode == false) {
                        Console.WriteLine("Error: status code 2eme request" + response2.StatusCode);
                        return NotFound("Error while getting user information");
                    }

                    var json = await response2.Content.ReadAsStringAsync();
                    var jsonObject = JObject.Parse(json);
                    Console.WriteLine("Json: " + jsonObject);
                    string email = jsonObject["email"].ToString();
                    string firstname = jsonObject["given_name"].ToString();
                    string name = jsonObject["family_name"].ToString();
                    string urlPhoto = jsonObject["picture"].ToString();

                    var googleServices = await _servicesRepository.GetAsync(s => s.Name == "Google");
                    var foundUser = await _userRepository.GetAsync(p => p.Email == email);
                    if (foundUser != null) {
                        Console.WriteLine("User found: " + foundUser.Email);
                        var key = _configuration.GetSection("AppSettings:SecretKey").Value;
                        var token = _tokenService.GenerateToken(foundUser.Id.ToString());
                        if (token == null)
                            return NotFound("Error while generating token");

                        var foundUserSite = await _userSitesRepository.GetAsync(u => (u.User.Id == foundUser.Id) && (u.Name == "Google"));
                        if (foundUserSite != null) {
                            foundUserSite.UserKey = tokenData["access_token"];
                            await _userSitesRepository.UpdateAsync(foundUserSite);
                        } else {
                            var newUserSite = new UserSites()
                            {
                                Name = "Google",
                                Email = foundUser.Email,
                                Services = googleServices,
                                User = foundUser,
                                DateCreation = DateTime.Now,
                                UserKey = tokenData["access_token"],
                            };

                            await _userSitesRepository.AddAsync(newUserSite);
                        }
                        return Ok(new { token });
                    } else {
                        var svc = new User() {
                            Firstname = firstname,
                            Lastname = name,
                            Email = email,
                            PhotoUrl = urlPhoto,
                            Password = BCrypt.Net.BCrypt.HashPassword("google2023"),
                            AccountStatus = UserAccountStatus.Active,
                            DateCreation = DateTime.UtcNow,
                            GoogleStatus = UserGoogleStatus.Linked
                        };
                        var result = await _userRepository.AddAsync(svc);
                        var key = _configuration.GetSection("AppSettings:SecretKey").Value;
                        var token = _tokenService.GenerateToken(result.Id.ToString());
                        if (token == null)
                            return NotFound("Error while generating token");
                      
                        var newUserSite = new UserSites() {
                            Name = "Google",
                            Email = svc.Email,
                            Services = googleServices,
                            User = svc,
                            DateCreation = DateTime.Now,
                            UserKey = tokenData["access_token"],
                        };

                        await _userSitesRepository.AddAsync(newUserSite);
                        return Ok(new { token });
                    }
                } else if (pageName == "account") {
                    if (tokenData["access_token"] == null)
                        return NotFound("Error: Access Token is null");
                    return Ok("OK");
                } else
                    return NotFound("Page not found: " + pageName);
            } catch (Exception e) {
                Console.WriteLine("Error try: " + e.Message);
                Response.Response = "Non";
                return NotFound(Response);
            }
        }
    }
}