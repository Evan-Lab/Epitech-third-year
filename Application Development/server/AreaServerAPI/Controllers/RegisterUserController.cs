using AreaServer.Core.Models;
using AreaServer.Infrastructure.Data.Repository;
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
    public class RegisterUserController : ControllerBase
    {
        public readonly ILogger<RegisterUserController> _logger;
        public IRepository<User> _userRepository;

        public RegisterUserController(IRepository<User> userRepository, ILogger<RegisterUserController> logger)
        {
            _logger = logger;
            _userRepository = userRepository;
        }

        [HttpPost("/auth/register", Name = "REGISTER_USER")]
        [SwaggerOperation(
              Summary = "Register a new user",
              Description = "Handle the registration of a new user: firstname, lastname, email, password, photoUrl",
              OperationId = "User.RegisterUser",
              Tags = new[] { "Authentification" })
        ]
        public async Task<ActionResult<User>> RegisterUser(RegisterNewUserRequest request)
        {
            // ATTENTION CECI EST UN TEST //
            string body = @"
<body class=""bg-light"">
  <div class=""container"">
    <img class=""ax-center my-10 w-24"" src=""https://cdn.discordapp.com/attachments/1122320337682579507/1166692360671997963/Logo.png?ex=654b69ee&is=6538f4ee&hm=fa0b685a8bd03194d5e6640018569edad2b42e24793c5b7237a5253616d05e61&"" />
    <div class=""card p-6 p-lg-10 space-y-4"">
      <h1 class=""h3 fw-700"">
        Craft your account !
      </h1>
      <p>
        Almost done! To complete your registration, please click the button below to verify your email address.
      </p>
      <a class=""btn btn-primary p-3 fw-700"" href=""http://localhost:8081/confirmation?Email={0}"">Verify email address</a>
    </div>
    <div class=""text-muted text-center my-6"">
      Sent with<br>
      01 Avenue des Champs-Élysées<br>
      Paris 75008, France <br>
    </div>
  </div>
</body>";

            string subject = "CraftArea - Registration";

            var svc = new User()
            {
                Firstname = request.FirstName,
                Lastname = request.LastName,
                Email = request.Email,
                PhotoUrl = request.PhotoUrl,
                Password = BCrypt.Net.BCrypt.HashPassword(request.Password),
                DateCreation = DateTime.UtcNow,
                GoogleStatus = UserGoogleStatus.NotLinked
            };

            try {
                Console.WriteLine("Body: " + body);
                body = body.Replace("{0}", request.Email);
            } catch (Exception e) {
                Console.WriteLine("Erreur fichier html: " + e.Message);
            }
            var foundUser = await _userRepository.GetAsync(p => p.Email == request.Email);
            var response = new ApiResponse();

            if (foundUser != null)
            {
                response.Response = "User already exist";
                return Conflict(response);
            }

            var result = await _userRepository.AddAsync(svc);
            var mailer = new SendMail("smtp.outlook.com", 587, "craft.area2@outlook.com", "CraftArea");
            mailer.SendEmail("craft.area2@outlook.com", request.Email, subject, body, -200);
            response.Response = "You're registered! Check your email to confirm and validate your registration";
            return Ok(response);
        }
    }
}
