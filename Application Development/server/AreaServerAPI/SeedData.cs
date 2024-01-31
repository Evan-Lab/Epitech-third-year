
using AreaServer.Core.Models;
using AreaServer.Core.Settings;
using AreaServer.Infrastructure.Data;
using AreaServer.Infrastructure.Data.Repository;
using Autofac.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace AreaServerAPI.Controllers;

public static class SeedData
{

    //private static IRequestManager _requestManager = null;
    private static IOptions<AppSettings> _config = null;
    private static IServiceProvider _serviceProvider = null;

    public static async Task Initialize(IServiceProvider serviceProvider)
    {
        using (var scope = serviceProvider.CreateScope())
        {
            var service = scope.ServiceProvider;

            using (var dbContext = new AppDbContext(service.GetRequiredService<IOptions<AppSettings>>(), service.GetRequiredService<DbContextOptions<AppDbContext>>()))
            {
                _serviceProvider = service;
                //_requestManager = service.GetRequiredService<IRequestManager>();
                _config = service.GetRequiredService<IOptions<AppSettings>>();


                await PopulateServiceDatas(dbContext);
            }
        }

    }

    public static async Task PopulateServiceDatas(AppDbContext dbContext)
    {
        var allServices = new List<Services>();
        var allReaction = new List<ReactionArea>();
        var allAction = new List<ActionArea>();
        var allArea = new List<UserArea>();
        var dateNow = DateTime.UtcNow;

        var userTest = new User()
        {
            Firstname = "Test",
            Lastname = "Test",
            Email = "test.test@epitech.eu",
            PhotoUrl = "",
            Password = BCrypt.Net.BCrypt.HashPassword("test"),
            DateCreation = dateNow,
            AccountStatus = UserAccountStatus.Active,
            GoogleStatus = UserGoogleStatus.NotLinked,
            Admin = UserAdminStatus.IsModerator,
        };

        /*---------------- ADDING SERVICES ---------------------*/

        var google = new Services()
        {
            Name = "Google",
            DateCreation = dateNow,
        };

        var github = new Services()
        {
            Name = "Github",
            DateCreation = dateNow,
        };

        var facebook = new Services()
        {
            Name = "Facebook",
            DateCreation = dateNow,
        };

        var spotify = new Services()
        {
            Name = "Spotify",
            DateCreation = dateNow,
        };

        var openWeather = new Services()
        {
            Name = "Open Weather",
            DateCreation = dateNow,
        };

        var outlook = new Services()
        {
            Name = "Outlook",
            DateCreation = dateNow,
        };

        var worldTime = new Services()
        {
            Name = "World Time",
            DateCreation = dateNow,
        };

        allServices.Add(facebook);
        allServices.Add(spotify);
        allServices.Add(github);
        allServices.Add(outlook);
        allServices.Add(openWeather);
        allServices.Add(google);
        allServices.Add(worldTime);

        /*--------------------- ADDING ACTIONS -------------------*/

        var OpenWeatherAction = new ActionArea()
        {
            Name = "Get Weather",
            DateCreation = dateNow,
            Service = openWeather,
        };

        var GithubAction_Push = new ActionArea()
        {
            Name = "Github:Push",
            DateCreation = dateNow,
            Service = github,
        };

        var GithubAction_PullRequest = new ActionArea()
        {
            Name = "Github:Pull Request",
            DateCreation = dateNow,
            Service = github,
        };

        var GithubAction_Repository = new ActionArea()
        {
            Name = "Github:New Repository",
            DateCreation = dateNow,
            Service = github,
        };

        var GithubAction_NewCollaborator = new ActionArea()
        {
            Name = "Github:New Collaborator",
            DateCreation = dateNow,
            Service = github,
        };

        var GithubAction_NewBranch = new ActionArea()
        {
            Name = "Github:New Branch",
            DateCreation = dateNow,
            Service = github,
        };

        var GithubAction_ClosePullRequest = new ActionArea()
        {
            Name = "Github:New Pull Request Closed",
            DateCreation = dateNow,
            Service = github,
        };

        var GithubAction_DeleteBranch = new ActionArea()
        {
            Name = "Github:Delete Branch",
            DateCreation = dateNow,
            Service = github,
        };

        var YoutubeAction_NewVideo = new ActionArea()
        {
            Name = "Youtube:New Video",
            DateCreation = dateNow,
            Service = google,
        };

        var YoutubeAction_NewFollower = new ActionArea()
        {
            Name = "Youtube:New Follower",
            DateCreation = dateNow,
            Service = google,
        };

        var YoutubeAction_NewPostOf = new ActionArea()
        {
            Name = "Youtube:New Post Of",
            DateCreation = dateNow,
            Service = google,
        };

        var SpotifyAction_NewPlaylist = new ActionArea()
        {
            Name = "Spotify:New Playlist",
            DateCreation = dateNow,
            Service = spotify,
        };

        var SpotifyAction_NewFollower = new ActionArea()
        {
            Name = "Spotify:New Follower",
            DateCreation = dateNow,
            Service = spotify,
        };

        var SpotifyAction_NewTrack = new ActionArea()
        {
            Name = "Spotify:New Track",
            DateCreation = dateNow,
            Service = spotify,
        };

        var WorldTimeAction = new ActionArea()
        {
            Name = "World Time",
            DateCreation = dateNow,
            Service = worldTime,
        };

        allAction.Add(OpenWeatherAction);
        allAction.Add(GithubAction_Push);
        allAction.Add(GithubAction_PullRequest);
        allAction.Add(GithubAction_Repository);
        allAction.Add(YoutubeAction_NewVideo);
        allAction.Add(YoutubeAction_NewFollower);
        allAction.Add(YoutubeAction_NewPostOf);
        allAction.Add(SpotifyAction_NewPlaylist);
        allAction.Add(SpotifyAction_NewFollower);
        allAction.Add(SpotifyAction_NewTrack);
        allAction.Add(GithubAction_NewCollaborator);
        allAction.Add(GithubAction_NewBranch);
        allAction.Add(GithubAction_ClosePullRequest);
        allAction.Add(GithubAction_DeleteBranch);
        allAction.Add(WorldTimeAction);

        /*-------------------- ADDING REACTIONS ------------------*/

        var SendMailReaction = new ReactionArea()
        {
            Name = "Send Email",
            DateCreation = dateNow,
            Service = outlook,
        };

        var SubscribeChannelYoutube = new ReactionArea()
        {
            Name = "Subscribe Channel Youtube",
            DateCreation = dateNow,
            Service = google,
        };

        var AddVideoInPlaylistYoutube = new ReactionArea()
        {
            Name = "Youtube:Add Video in Playlist",
            DateCreation = dateNow,
            Service = google,
        };

        var followArtistSpotify = new ReactionArea()
        {
            Name = "Spotify:Follow Artist",
            DateCreation = dateNow,
            Service = spotify,
        };

        var followPlaylistSpotify = new ReactionArea()
        {
            Name = "Spotify:Follow PLaylist",
            DateCreation = dateNow,
            Service = spotify,
        };

        var addTrackInPlaylistSpotify = new ReactionArea()
        {
            Name = "Spotify:Add track in playlist",
            DateCreation = dateNow,
            Service = spotify,
        };

        allReaction.Add(SendMailReaction);
        allReaction.Add(SubscribeChannelYoutube);
        allReaction.Add(AddVideoInPlaylistYoutube);
        allReaction.Add(addTrackInPlaylistSpotify);
        allReaction.Add(followArtistSpotify);
        allReaction.Add(followPlaylistSpotify);

        /*------------------------------------------------------*/


        allServices = allServices.OrderBy(p => p.Name).ToList();

        allReaction = allReaction.OrderBy(p => p.Name).ToList();

        allAction = allAction.OrderBy(p => p.Name).ToList();

        await dbContext.User.AddAsync(userTest);
        await dbContext.Services.AddRangeAsync(allServices);
        await dbContext.ReactionArea.AddRangeAsync(allReaction);
        await dbContext.ActionAreas.AddRangeAsync(allAction);
        await dbContext.SaveChangesAsync();
    }

}
