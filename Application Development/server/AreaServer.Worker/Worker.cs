using AreaServer.Core.Class;
using AreaServer.Core.Models;
using AreaServer.Infrastructure.Data;
using AreaServer.Infrastructure.Data.Repository;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Linq.Expressions;

namespace AreaServer.Worker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly AppDbContext _dbContext;

        public Worker(ILogger<Worker> logger, AppDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        private async Task<string> getAccessToken(int userId, string services)
        {
            var items = await _dbContext.UserSites.Where(u => u.User.Id ==  userId && u.Name == services).ToListAsync();
            return items[0].UserKey;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var items = await _dbContext.UserAreas.Include(u => u.User).Include(u => u.ActionArea).Include(u => u.ReactionArea).ToListAsync();

                foreach (var userArea in items)
                {
                    if (userArea != null && userArea.IsActive == 1)
                    {
                        DateTime now = DateTime.Now;
                        dynamic parameters = null;
                        dynamic response = null;
                        int hour = now.Hour;
                        int minute = now.Minute;
                        if (userArea.ParamAction != string.Empty)
                        {
                            parameters = JsonConvert.DeserializeObject<dynamic>(userArea.ParamAction);
                            if (parameters.hour != null && parameters.minute != null)
                            {
                                hour = parameters.hour - 2;
                                minute = parameters.minute;
                            }
                        }

                        if (hour <= now.Hour && minute <= now.Minute)
                        {
                            /* ----------- OPEN WEATHER ------------- */
                            if (userArea.ActionArea.Name == "Get Weather")
                            {
                                var doAction = new WeatherAction();
                                if (parameters.city != null)
                                {
                                    string city = parameters.city.ToString();
                                    dynamic weather = await doAction.getWeather(city);
                                    response = JsonConvert.DeserializeObject<dynamic>(weather);
                                }
                            }

                            /* ------------ World Time ---------------- */

                            if (userArea.ActionArea.Name == "World Time")
                            {
                                var doAction = new WorldTimeAction();
                                if (parameters.day != null && parameters.month != null && parameters.year != null && parameters.hourParam != null && parameters.minuteParam != null)
                                {
                                    var day = parameters.day.ToObject<int>();
                                    var month = parameters.month.ToObject<int>();
                                    var year = parameters.year.ToObject<int>();
                                    var hourParam = parameters.hourParam.ToObject<int>();
                                    var minuteParam = parameters.minuteParam.ToObject<int>();
                                    dynamic time = await doAction.getWorldTime(day, month, year, hourParam, minuteParam);
                                    response = JsonConvert.DeserializeObject<dynamic>(time);
                                }
                            }

                            /* ---------------- SPOTIFY ------------------------- */

                            if (userArea.ActionArea.Name == "Spotify:New Playlist")
                            {
                                var doAction = new SpotifyAction();
                                var accessToken = await getAccessToken(userArea.User.Id, "Spotify");

                                if (userArea.InfoAction == string.Empty)
                                {

                                    dynamic resFirst = await doAction.HandleFirstRequest(accessToken, "", "playlist");
                                    userArea.InfoAction = resFirst;
                                }

                                dynamic resDB = JsonConvert.DeserializeObject<dynamic>(userArea.InfoAction);
                                if (resDB != null && resDB.error == false && resDB.value != null && resDB.value.comparator != null)
                                {
                                    await Task.Delay(5000, stoppingToken);
                                    dynamic res = await doAction.HandleNewPlaylist(accessToken, resDB.value.comparator.ToString());
                                    response = JsonConvert.DeserializeObject<dynamic>(res);
                                }
                            }

                            if (userArea.ActionArea.Name == "Spotify:New Follower")
                            {
                                var doAction = new SpotifyAction();
                                var accessToken = await getAccessToken(userArea.User.Id, "Spotify");
                                if (userArea.InfoAction == string.Empty)
                                {
                                    dynamic resFirst = await doAction.HandleFirstRequest(accessToken, "", "follower");
                                    userArea.InfoAction = resFirst;
                                }

                                dynamic resDB = JsonConvert.DeserializeObject<dynamic>(userArea.InfoAction);
                                if (resDB != null && resDB.error == false && resDB.value != null && resDB.value.comparator != null)
                                {
                                    await Task.Delay(5000, stoppingToken);
                                    dynamic res = await doAction.HandleNewFollower(accessToken, resDB.value.comparator.ToString());
                                    response = JsonConvert.DeserializeObject<dynamic>(res);
                                }
                            }

                            if (userArea.ActionArea.Name == "Spotify:New Track")
                            {
                                var doAction = new SpotifyAction();
                                if (parameters.url != null)
                                {
                                    var accessToken = await getAccessToken(userArea.User.Id, "Spotify");
                                    if (userArea.InfoAction == string.Empty)
                                    {
                                        dynamic resFirst = await doAction.HandleFirstRequest(accessToken, parameters.url.ToString(), "track");
                                        userArea.InfoAction = resFirst;
                                    }

                                    dynamic resDB = JsonConvert.DeserializeObject<dynamic>(userArea.InfoAction);
                                    if (resDB != null && resDB.error == false && resDB.value != null && resDB.value.comparator != null)
                                    {
                                        await Task.Delay(5000, stoppingToken);
                                        dynamic res = await doAction.HandleNewTrack(accessToken, parameters.url.ToString(), resDB.value.comparator.ToString());
                                        response = JsonConvert.DeserializeObject<dynamic>(res);
                                    }
                                }
                            }
                            /* ----------------- GITHUB ----------------------------- */
                            if (userArea.ActionArea.Name == "Github:Push")
                            {
                                if (parameters.name != null)
                                {
                                    var doAction = new GithubAction();
                                    var accessToken = await getAccessToken(userArea.User.Id, "Github");

                                    dynamic res = await doAction.HandleGithubAction(accessToken, parameters.name.ToString(), userArea.DateCreation, 1);
                                    response = JsonConvert.DeserializeObject<dynamic>(res);
                                }
                            }

                            if (userArea.ActionArea.Name == "Github:Pull Request")
                            {
                                if (parameters.name != null)
                                {
                                    var doAction = new GithubAction();
                                    var accessToken = await getAccessToken(userArea.User.Id, "Github");

                                    dynamic res = await doAction.HandleGithubAction(accessToken, parameters.name.ToString(), userArea.DateCreation, 3);
                                    response = JsonConvert.DeserializeObject<dynamic>(res);
                                }
                            }

                            if (userArea.ActionArea.Name == "Github:New Repository")
                            {
                                var doAction = new GithubAction();
                                var accessToken = await getAccessToken(userArea.User.Id, "Github");

                                dynamic res = await doAction.HandleGithubAction(accessToken, "", userArea.DateCreation, 2);
                                response = JsonConvert.DeserializeObject<dynamic>(res);
                            }

                            if (userArea.ActionArea.Name == "Github:New Collaborator")
                            {
                                if (parameters.name != null)
                                {
                                    var doAction = new GithubAction();
                                    var accessToken = await getAccessToken(userArea.User.Id, "Github");

                                    dynamic res = await doAction.HandleGithubAction(accessToken, parameters.name.ToString(), userArea.DateCreation, 6);
                                    response = JsonConvert.DeserializeObject<dynamic>(res);
                                }
                            }

                            if (userArea.ActionArea.Name == "Github:New Branch")
                            {
                                if (parameters.name != null)
                                {
                                    var doAction = new GithubAction();
                                    var accessToken = await getAccessToken(userArea.User.Id, "Github");

                                    dynamic res = await doAction.HandleGithubAction(accessToken, parameters.name.ToString(), userArea.DateCreation, 7);
                                    response = JsonConvert.DeserializeObject<dynamic>(res);
                                }
                            }

                            if (userArea.ActionArea.Name == "Github:New Pull Request Closed")
                            {
                                if (parameters.name != null)
                                {
                                    var doAction = new GithubAction();
                                    var accessToken = await getAccessToken(userArea.User.Id, "Github");

                                    dynamic res = await doAction.HandleGithubAction(accessToken, parameters.name.ToString(), userArea.DateCreation, 4);
                                    response = JsonConvert.DeserializeObject<dynamic>(res);
                                }
                            }

                            if (userArea.ActionArea.Name == "Github:Delete Branch")
                            {
                                if (parameters.name != null)
                                {
                                    var doAction = new GithubAction();
                                    var accessToken = await getAccessToken(userArea.User.Id, "Github");

                                    dynamic res = await doAction.HandleGithubAction(accessToken, parameters.name.ToString(), userArea.DateCreation, 5);
                                    response = JsonConvert.DeserializeObject<dynamic>(res);
                                }
                            }

                            /* ---------------- YOUTUBE ----------------------- */

                            if (userArea.ActionArea.Name == "Youtube:New Follower")
                            {
                                var doAction = new YoutubeAction();
                                var accessToken = await getAccessToken(userArea.User.Id, "Google");
                                Console.WriteLine("Premier if");
                                if (userArea.InfoAction == string.Empty)
                                {
                                    dynamic resFirst = await doAction.HandleFirstRequest_Follower(accessToken);
                                    userArea.InfoAction = resFirst;
                                }

                                dynamic resDB = JsonConvert.DeserializeObject<dynamic>(userArea.InfoAction);
                                if (resDB != null && resDB.error == false && resDB.value != null && resDB.value.comparator != null)
                                {
                                    await Task.Delay(5000, stoppingToken);
                                    dynamic res = await doAction.HandleSecRequest_Follower(accessToken, resDB.value.comparator.ToObject<int>());
                                    response = JsonConvert.DeserializeObject<dynamic>(res);
                                }
                            }

                            if (userArea.ActionArea.Name == "Youtube:New Video")
                            {
                                var doAction = new YoutubeAction();
                                var accessToken = await getAccessToken(userArea.User.Id, "Google");
                                if (userArea.InfoAction == null)
                                {
                                    dynamic resFirst = await doAction.HandleFirstRequest_NewVideo(accessToken);
                                    userArea.InfoAction = resFirst;
                                }

                                dynamic resDB = JsonConvert.DeserializeObject<dynamic>(userArea.InfoAction);
                                if (resDB != null && resDB.error == false && resDB.value != null && resDB.value.comparator != null)
                                {
                                    await Task.Delay(5000, stoppingToken);
                                    dynamic res = await doAction.HandleSecRequest_NewVideo(accessToken, resDB.value.comparator.ToObject<int>());
                                    response = JsonConvert.DeserializeObject<dynamic>(res);
                                }
                            }
                        }

                        /* ------------------ REACTION ------------------------ */
                        if (response != null && response.error == false)
                        {
                            parameters = JsonConvert.DeserializeObject<dynamic>(userArea.ParamReaction);
                            bool finished = false;

                            if (userArea.ReactionArea.Name == "Send Email")
                            {
                                var doAction = new SendMailAction();
                                string body = "This is your information:\n" + response.value.ToString();
                                finished = doAction.SendEmail(parameters.email.ToString(), body, userArea.Name);

                                Console.WriteLine(finished);
                            }
                            /* ----------------- YOUTUBE ----------------- */
                            if (userArea.ReactionArea.Name == "Subscribe Channel Youtube")
                            {
                                Console.WriteLine("PASS HERE");
                                if (parameters.url != null)
                                {
                                    var url = parameters.url.ToString();
                                    var accessToken = await getAccessToken(userArea.User.Id, "Google");
                                    var doAction = new SubscribeChannelYoutube();
                                    finished = await doAction.SubscribeChannel(url, accessToken);
                                }
                            }

                            if (userArea.ReactionArea.Name == "Youtube:Add Video in Playlist")
                            {
                                if (parameters.urlVideo != null && parameters.urlPlaylist != null)
                                {
                                    var urlVideo = parameters.urlVideo.ToString();
                                    var urlPlaylist = parameters.urlPlaylist.ToString();
                                    var accessToken = await getAccessToken(userArea.User.Id, "Google");
                                    var doAction = new AddVideoInPLaylistYoutubeReaction();
                                    finished = await doAction.AddVideoInPlaylist(urlVideo, urlPlaylist, accessToken);
                                }
                            }

                            /* ---------------- SPOTIFY ------------------ */

                            if (userArea.ReactionArea.Name == "Spotify:Follow Artist")
                            {
                                if (parameters.url != null)
                                {
                                    var url = parameters.url.ToString();
                                    var accessToken = await getAccessToken(userArea.User.Id, "Spotify");
                                    var doAction = new SpotifyReaction(accessToken);
                                    finished = await doAction.FollowArtist(url);
                                }
                            }

                            if (userArea.ReactionArea.Name == "Spotify:Follow PLaylist")
                            {
                                if (parameters.url != null)
                                {
                                    var url = parameters.url.ToString();
                                    var accessToken = await getAccessToken(userArea.User.Id, "Spotify");
                                    var doAction = new SpotifyReaction(accessToken);
                                    finished = await doAction.FollowPlaylist(url);
                                }
                            }

                            if (userArea.ReactionArea.Name == "Spotify:Add track in playlist")
                            {
                                if (parameters.urlPlaylist != null && parameters.urlTrack != null)
                                {
                                    var urlPlaylist = parameters.urlPlaylist.ToString();
                                    var urlTrack = parameters.urlTrack.ToString();
                                    var accessToken = await getAccessToken(userArea.User.Id, "Spotify");
                                    var doAction = new SpotifyReaction(accessToken);
                                    finished = await doAction.AddTrackInPlaylist(urlPlaylist, urlTrack);
                                }
                            }
                            /* ---------------------- RESULT --------------------- */
                            if (finished == true)
                            {
                                var existingUserArea = await _dbContext.UserAreas.FindAsync(userArea.Id);
                                
                                userArea.DateCreation = userArea.DateCreation.ToUniversalTime();
                                userArea.IsActive = 0;

                                existingUserArea.IsActive = userArea.IsActive;
                                existingUserArea.DateCreation = userArea.DateCreation;
                                //_dbContext.UserAreas.Update(userArea);
                                //_dbContext.UserAreas.ExecuteUpdateAsync(userArea);
                                _dbContext.UserAreas.Update(existingUserArea);
                                await _dbContext.SaveChangesAsync();
                            }
                        }
                    }
                    _logger.LogInformation("Worker running at: {time}, AREA NAME: {name}, ParamAction: {param}, ParamReaction: {react}",
                        DateTimeOffset.Now, userArea.Name, userArea.ParamAction, userArea.ParamReaction);
                    await Task.Delay(5000, stoppingToken);
                }

                await Task.Delay(5000, stoppingToken);
            }
        }
    }
}
