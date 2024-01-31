using AreaServer.Core.Models;
using AreaServer.Infrastructure.Data.Repository;
using Swashbuckle.AspNetCore.Annotations;
using AreaServerAPI.Objects.Requests;
using AreaServerAPI.Objects.Responses;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text.Json.Serialization;
using System;
using Microsoft.AspNetCore.Authorization;

namespace AreaServerAPI.Controllers
{
    [Authorize]
    public class EditPasswordController : ControllerBase
    {
        public readonly ILogger<EditPasswordController> _logger;
        public IRepository<User> _userRepository;

        public EditPasswordController(IRepository<User> userRepository, ILogger<EditPasswordController> logger)
        {
            _logger = logger;
            _userRepository = userRepository;
        }

        [HttpPost("/user/resetPwd", Name = "EDIT_PASSWORD")]
        [SwaggerOperation(
              Summary = "Edit password of a user",
              Description = "Handle the edit password of a user: id, password",
              OperationId = "User.EditPassword",
              Tags = new[] { "User" })
        ]
        public async Task<ActionResult<string>> EditPassword(EditPasswordRequest request)
        {
            var foundUser = await _userRepository.GetAsync(p => p.Id == request.Id);
            var response = new ApiResponse();

            if (foundUser == null) {
                response.Response = "User not found";
                return NotFound(response);
            } else {
                foundUser.Password = BCrypt.Net.BCrypt.HashPassword(request.Password);
                await _userRepository.UpdateAsync(foundUser);
                response.Response = "Password updated";
                return Ok(response);
            }
        }
    }
}