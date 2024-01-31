using System;
using System.Text.Json.Serialization;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;
using Microsoft.Extensions.DependencyInjection;

public class YoutubeAction
{
    public YoutubeAction() {}

    public async Task<dynamic> GetChannelInfo(YouTubeService youtubeService)
    {
        try
        {
            var channelRequest = youtubeService.Channels.List("snippet,statistics,contentDetails");
            channelRequest.Mine = true;
            var channelResponse = await channelRequest.ExecuteAsync();

            if (channelResponse.Items.Count > 0)
            {
                var myChannel = channelResponse.Items[0];

                string channelTitle = myChannel.Snippet.Title;
                string channelDescription = myChannel.Snippet.Description;
                ulong subscriberCount = myChannel.Statistics.SubscriberCount ?? 0;
                string uploadPlaylistId = myChannel.ContentDetails.RelatedPlaylists.Uploads;
                string channelId = myChannel.Id;

                var result = new { error = false, value = new 
                    {
                        channelId,
                        title = channelTitle,
                        description = channelDescription,
                        subscribers = subscriberCount,
                        uploadPlaylistId
                    }
                };
                return result;
            }
            else
            {
                Console.WriteLine("Aucune chaîne trouvée.");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("Error: " + e.Message);
        }

        var errorResult = new { error = true, value = new
            {
                channelId = string.Empty,
                title = string.Empty,
                description = string.Empty,
                subscribers = 0UL,
                uploadPlaylistId = string.Empty
            }
        };

        return errorResult;
    }

    public async Task<string> HandleFirstRequest_Follower(string AccessToken)
    {
        bool IsError = true;
        int count = -1;
        try {
            var credentials = GoogleCredential.FromAccessToken(AccessToken).CreateScoped(YouTubeService.Scope.YoutubeReadonly);
            var youtubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credentials,
                ApplicationName = "YouTube Subscriber Checker"
            });

            var channelRequest = youtubeService.Channels.List("statistics");
            channelRequest.Mine = true;
            var channelResponse = await channelRequest.ExecuteAsync();
            if (channelResponse.Items.Count > 0)
            {
                var myChannel = channelResponse.Items[0];
                ulong? subscriberCountLong = myChannel.Statistics.SubscriberCount;
                if (subscriberCountLong.HasValue) {
                    string subscriberCount = subscriberCountLong.Value.ToString();
                    if (int.TryParse(subscriberCount, out count))
                        IsError = false;
                    else {
                        Console.WriteLine("Error: Can't parse subscriber count");
                        IsError = true;
                    }
                } else {
                    Console.WriteLine("Error: Subscriber count is null");
                    IsError = true;
                }
            } else {
                Console.WriteLine("Aucune chaîne trouvée.");
                IsError = true;
            }
        } catch (Exception e) {
            Console.WriteLine("Error try: " + e.Message);
            IsError = true;
        }

        var result = new { error = IsError, value = new { comparator = count } };
        return Newtonsoft.Json.JsonConvert.SerializeObject(result);
    }

    public async Task<string> HandleSecRequest_Follower(string access_token, int firstCount)
    {
        dynamic response = await HandleFirstRequest_Follower(access_token);
        dynamic data = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(response);
        bool isError = data.error;
        int count = data.value.comparator;
        if (isError == false && count != -1) {
            if (count > firstCount) {
                Console.WriteLine("New follower detected");
                return Newtonsoft.Json.JsonConvert.SerializeObject(new { error = false, value = new { } });
            } else
                return Newtonsoft.Json.JsonConvert.SerializeObject(new { error = true, value = new { } });
        } else
            return Newtonsoft.Json.JsonConvert.SerializeObject(new { error = true, value = new { } });

    }

    public async Task<string> HandleFirstRequest_NewVideo(string access_token)
    {
        bool isError = true;
        int count = -1;
        try {
            var credentials = GoogleCredential.FromAccessToken(access_token).CreateScoped(YouTubeService.Scope.YoutubeReadonly);
            var youtubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credentials,
                ApplicationName = "YouTube New Video Checker"
            });

            var channelInfo = await GetChannelInfo(youtubeService);

            if (channelInfo.error == false) {
                string channelId = channelInfo.value.channelId;
                var playlistItemsRequest = youtubeService.PlaylistItems.List("snippet");
                playlistItemsRequest.PlaylistId = channelInfo.value.uploadPlaylistId;
                playlistItemsRequest.MaxResults = 10;
                var playlistItemsResponse = await playlistItemsRequest.ExecuteAsync();

                var videoList = playlistItemsResponse.Items.Select(video => video.Snippet.Title).ToList();
                foreach (var video in videoList)
                {
                    Console.WriteLine(video);
                }
                count = videoList.Count;

                isError = false;
            } else {
                Console.WriteLine("Error: " + channelInfo.value);
                isError = true;
            }
        } catch (Exception e) {
            Console.WriteLine("Error try: " + e.Message);
            isError = true;
        }
        var eroro = new { error = isError, value = new { comparator = count } };
        return Newtonsoft.Json.JsonConvert.SerializeObject(eroro);
    }

    public async Task<string> HandleSecRequest_NewVideo(string access_token, int lastVideoCount)
    {   
        dynamic response = await HandleFirstRequest_NewVideo(access_token);
        dynamic data = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(response);
        bool isError = data.error;
        int newVideoCount = data.value.comparator;
        
        if (!isError && newVideoCount != -1)
        {
            if (newVideoCount > lastVideoCount) {
                Console.WriteLine("New Video detected");
                return Newtonsoft.Json.JsonConvert.SerializeObject(new { error = false, value = new { } });
            }
        }  
        var result = new { error = true, value = new {  } };
        return Newtonsoft.Json.JsonConvert.SerializeObject(result);
    }
}