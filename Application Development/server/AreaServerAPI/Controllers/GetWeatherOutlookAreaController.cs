using AreaServer.Core.Models;
using AreaServer.Infrastructure.Data.Repository;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Newtonsoft.Json.Linq;
using AreaServerAPI.Objects.Responses;
using AreaServerAPI.Objects.Requests;
using Microsoft.AspNetCore.Authorization;

namespace AreaServerAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class GetWeatherOutlookAreaController : ControllerBase
    {
        private readonly ILogger<GetWeatherOutlookAreaController> _logger;
        private IRepository<User> _userRepository;

        public GetWeatherOutlookAreaController(IRepository<User> userRepository, ILogger<GetWeatherOutlookAreaController> logger)
        {
            _logger = logger;
            _userRepository = userRepository;
        }

        [HttpPost("/area/tester", Name = "GET_WEATHER_OUTLOOK_AREA")]
        [SwaggerOperation(
              Summary = "Get status of weather outlook area",
              Description = "Give the status of the weather outlook area: email, city (Paris, London, etc...), hour, minute",
              OperationId = "User.GetWeatherOutlookArea",
              Tags = new[] { "Area tester" })
        ]
        public async Task<ActionResult<List<string>>> GetWeatherOutlookArea(EmailRequest request)
        {
            var Response = new ApiResponse();
            string smtpServer = "smtp.outlook.com";
            int port = 587;
            string username = "craft.area2@outlook.com";
            string password = "CraftArea";
            string from = "craft.area2@outlook.com";
            string subject = "CraftArea - Weather Outlook";
            string Message = "OK";
            bool active = true;
            int hour = request.Hour - 2;
            int minute = request.Minute;
            DateTime now = DateTime.Now;

            Console.WriteLine("Weather Outlook Area running at " + now.Hour + ":" + now.Minute + "...");
            while (active) {
                now = DateTime.Now;
                if (now.Hour == hour && now.Minute == minute) {
                    active = false;
                }
            }
            try {
                string to = request.Email;
                string city = request.City;
                string body = "The weather in " + city + " is: ";

                using (HttpClient httpClient = new HttpClient()) {
                    Console.WriteLine("Sending API request...");
                    var weather = new Weather();
                    double temperature = await weather.getWeather(city);
                    if (temperature != -1000) {
                        Console.WriteLine("API request successful!");
                        Console.WriteLine("Sending email to " + to + "...");
                        if (now.Hour == hour && now.Minute == minute) {
                            var mailer = new SendMail(smtpServer, port, username, password);
                            mailer.SendEmail(from, to, subject, body, temperature);
                        }
                        Console.WriteLine("Email sent!");
                    } else {
                        Console.WriteLine($"API request failed with status code: {temperature}");
                        Response.Response = "Non";
                        return NotFound(Response);
                    }
                }
            } catch (Exception ex) {
                Console.WriteLine($"Error: {ex.Message}");
                Response.Response = "Non";
                return NotFound(Response);
            }
            return Ok(Message);
        }
    }
}