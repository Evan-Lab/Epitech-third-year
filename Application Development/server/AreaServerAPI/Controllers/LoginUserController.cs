using AreaServer.Core.Models;
using AreaServer.Infrastructure.Data.Repository;
using AreaServerAPI.Objects.Requests;
using AreaServerAPI.Objects.Responses;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace AreaServerAPI.Controllers
{
    public class LoginUserController : ControllerBase
    {
        public readonly ILogger<LoginUserController> _logger;
        public IRepository<User> _userRepository;

        private readonly TokenService _tokenService;

        private readonly IConfiguration _configuration;

        public LoginUserController(IRepository<User> userRepository, ILogger<LoginUserController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _userRepository = userRepository;
            _tokenService = new TokenService(configuration);
            _configuration = configuration;
        }

        [HttpPost("/auth/login", Name = "LOGIN_USER")]
        [SwaggerOperation(
              Summary = "Login a new user",
              Description = "Handle the login of a new user: email, password",
              OperationId = "User.LoginUser",
              Tags = new[] { "Authentification" })
        ]
        public async Task<ActionResult<User>> LoginUser(LoginUserRequest request)
        {
            var svc = new User()
            {
                Email = request.Email,
                Password = request.Password
            };

            var foundUser = await _userRepository.GetAsync(p => p.Email == request.Email);
            var response = new ApiResponse();

            if (foundUser == null) 
            {
                response.Response = "User not register";
                return NotFound(response);
            }

            if (!BCrypt.Net.BCrypt.Verify(request.Password, foundUser.Password))
            {
                response.Response = "Bad email or password";
                return BadRequest(response);
            }

            if (foundUser.AccountStatus == UserAccountStatus.ConfirmationPending)
            {
                response.Response = "Email not confirmed";
                return BadRequest(response);
            }

            if (foundUser.AccountStatus == UserAccountStatus.Disabled)
            {
                response.Response = "User disabled";
                return BadRequest(response);
            }

            var key = _configuration.GetSection("AppSettings:SecretKey").Value;
            var token = _tokenService.GenerateToken(foundUser.Id.ToString());
            response.Response = "You are connected !";
            return Ok(new { token });
        }
    }
}
