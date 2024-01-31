using AreaServer.Core.Models;
using AreaServer.Infrastructure.Data.Repository;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using AreaServerAPI.Objects.Responses;
using AreaServerAPI.Objects.Requests;
using System.Threading.Tasks;
using System.Text.Json;
using Newtonsoft.Json;

namespace AreaServerAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RunNewActionController : ControllerBase
    {
        private readonly ILogger<RunNewActionController> _logger;
        private IRepository<User> _userRepository;

        public RunNewActionController(IRepository<User> userRepository, ILogger<RunNewActionController> logger)
        {
            _logger = logger;
            _userRepository = userRepository;
        }

        [HttpPost("/test/runAction", Name = "GET_ACTION_RUN")]
        [SwaggerOperation(
              Summary = "Login with Github",
              Description = "Get the link to login with Github",
              OperationId = "User.GetActionRun",
              Tags = new[] { "Test" })
        ]
        public async Task<ActionResult<List<string>>> RunActionTester(GithubActionRequest request)
        {
            GithubAction githubController = new GithubAction();
            SpotifyAction spotifyController = new SpotifyAction();
            YoutubeAction youtubeAction = new YoutubeAction();
            DateTime programStartTime = DateTime.Now;
            WorldTimeAction worldTimeAction = new WorldTimeAction();

            // dynamic response = await youtubeAction.HandleFirstRequest_NewVideoSpeChannel(request.AccessToken, request.RepoName);
            // dynamic json = JsonConvert.DeserializeObject<dynamic>(response);
            // int count = json.value.comparator;
            while (true) {
                Console.WriteLine("Waiting for push...");
                string res = await githubController.HandleGithubAction(request.AccessToken, request.RepoName, programStartTime, 3);
                dynamic json = JsonConvert.DeserializeObject<dynamic>(res);
                bool isError = json.error;
                if (isError == false) {
                    Console.WriteLine("Call api done");
                    break;
                }
                await Task.Delay(5000);
            }
            return Ok("OK");
        }
    }
}