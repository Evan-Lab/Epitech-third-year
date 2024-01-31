using AreaServer.Core.Models;
using AreaServer.Infrastructure.Data.Repository;
using AreaServerAPI.Objects.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Net;


namespace AreaServerAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RedirectToRouteController : ControllerBase
    {
        public readonly ILogger<RedirectToRouteController> _logger;
        public IRepository<User> _userRepository;

        public RedirectToRouteController(IRepository<User> userRepository, ILogger<RedirectToRouteController> logger)
        {
            _logger = logger;
            _userRepository = userRepository;
        }

        [HttpGet("/redirect/", Name = "REDIRECT_TO_ROUTE")]
        [ProducesResponseType((int)HttpStatusCode.MovedPermanently)]
        [SwaggerOperation(
              Summary = "Redirect to route",
              Description = "Redirect the user to the mobile route app",
              OperationId = "User.Redirect",
              Tags = new[] { "Mobile" })
        ]
        public IActionResult RedirectToRoute()
        {
            return RedirectPermanent("exp://enerlg.asuki.8082.exp.direct");
        }
    }
}
