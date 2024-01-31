using AreaServer.Core.Models;
using AreaServer.Infrastructure.Data.Repository;
using AreaServerAPI.Objects.Requests;
using AreaServerAPI.Objects.Responses;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace AreaServerAPI.Controllers
{
    public class EmailConfirmationController : ControllerBase
    {
        public readonly ILogger<EmailConfirmationController> _logger;
        public IRepository<User> _userRepository;

        public EmailConfirmationController(IRepository<User> userRepository, ILogger<EmailConfirmationController> logger)
        {
            _logger = logger;
            _userRepository = userRepository;
        }

        [HttpGet("/auth/confirmation", Name = "EMAIL_CONFIRMATION")]
        [SwaggerOperation(
              Summary = "Get response of email confirmation",
              Description = "Give the response of email confirmation: true or false",
              OperationId = "User.GetEmailConfirmation",
              Tags = new[] { "Authentification" })
        ]
        public async Task<ActionResult<List<string>>> GetEmailConfirmation(EmailConfirmationRequest request)
        {
            var response = new ApiResponse();
            var user = await _userRepository.GetAsync(u => u.Email == request.Email);
            bool set = request.Set;

            if (user == null)
            {
                response.Response = "User introuvable";
                return NotFound(response);
            }

            if (set == true)
            {
                user.AccountStatus = UserAccountStatus.Active;
                user.DateCreation = DateTime.UtcNow;
                await _userRepository.UpdateAsync(user);
                response.Response = "Email confirmed";
                return Ok(response);
            } else {
                response.Response = "Email not confirmed";
                return Ok(response);
            }
        }
    }
}