using AreaServer.Core.Models;
using AreaServer.Infrastructure.Data.Repository;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Newtonsoft.Json;
using System.IO;

namespace AreaServerAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GetAboutJsonFile : ControllerBase
    {
        private readonly ILogger<GetAboutJsonFile> _logger;
        private IRepository<Services> _servicesRepository;
        private IRepository<ReactionArea> _reactionRepository;
        private IRepository<ActionArea> _actionRepository;

        public GetAboutJsonFile(IRepository<Services> servicesRepository, IRepository<ReactionArea> reactionRepository, IRepository<ActionArea> actionRepository, ILogger<GetAboutJsonFile> logger)
        {
            _logger = logger;
            _actionRepository = actionRepository;
            _servicesRepository = servicesRepository;
            _reactionRepository = reactionRepository;
        }

        [HttpGet("/about.json", Name = "GET_ABOUT_JSON")]
        [SwaggerOperation(
              Summary = "Get about.json file",
              Description = "Get about.json file",
              OperationId = "Server.GetAboutJsonFile",
              Tags = new[] { "About.json File" })
        ]
        public async Task<ActionResult<string>> GetActionList()
        {
            try
            {
                var clientIp = HttpContext.Connection.RemoteIpAddress?.ToString();
                var services = await _servicesRepository.ListAsync();
                var actionsList = await _actionRepository.ListAsync();
                var reactionList = await _reactionRepository.ListAsync();

                var data = new
                {
                    client = new
                    {
                        host = clientIp?.Substring(7),
                    },
                    server = new
                    {
                        current_time = DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
                        services = services?.Select(s => new
                        {
                            name = s.Name,
                            actions = actionsList.Where(a => a.Service.Name == s.Name).Select(a => new
                            {
                                name = a.Name,
                                description = a.Description
                            }),
                            reactions = reactionList.Where(r => r.Service.Name == s.Name).Select(r => new
                            {
                                name = r.Name,
                                description = r.Description
                            })
                        })
                    }
                };
                var jsonResult = JsonConvert.SerializeObject(data, Formatting.Indented);

                var filePath = "../../about.json";
                System.IO.File.WriteAllText(filePath, jsonResult);
                return Content(jsonResult, "application/json");
            } catch (Exception ex)
            {
                Console.Write($"Error: {ex.Message}");
                return BadRequest("Error");
            }
        }

    }
}