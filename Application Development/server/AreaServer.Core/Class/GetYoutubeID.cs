using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using Octokit;

namespace AreaServer.Core.Class
{
    public class GetYoutubeID
    {
        public GetYoutubeID() { }

        public string GetVideoId(string url)
        {
            if (url.Contains("youtube.com"))
            {
                int startIndex = url.IndexOf("v=") + 2;
                int length = url.Length - startIndex;
                int endIndex = url.IndexOf("&", startIndex, length);
                if (endIndex == -1)
                {
                    endIndex = url.Length;
                }

                return url.Substring(startIndex, endIndex - startIndex);
            }
            else if (url.Contains("youtu.be"))
            {
                int startIndex = url.LastIndexOf("/") + 1;
                return url.Substring(startIndex);
            }

            return null;
        }

        private string GetChannelId(string url)
        {
            int startIndex = url.LastIndexOf("/") + 1;
            return url.Substring(startIndex);
        }

        public string GetChannelIdFromUsername(string url, string apiKey)
        {
            var credentials = GoogleCredential.FromAccessToken(apiKey).CreateScoped(YouTubeService.Scope.YoutubeReadonly);
            var youtubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credentials,
                ApplicationName = "get channelid",
            });

            var username = GetChannelId(url);

            Console.WriteLine($"USERNAME: {username}");
            if (url.Contains("youtube.com/channel/"))
            {
                return username;
            }

            ChannelsResource.ListRequest channelsRequest = youtubeService.Channels.List("snippet");
            channelsRequest.ForUsername = username;

            var channelsResponse = channelsRequest.Execute();

            if (channelsResponse.Items.Count > 0)
            {
                return channelsResponse.Items[0].Id;
            }

            return null;
        }

        public string GetPlaylistId(string url)
        {
            if (url.Contains("youtube.com/playlist?list="))
            {
                int startIndex = url.IndexOf("list=") + 5;
                int length = url.Length - startIndex;
                int endIndex = url.IndexOf("&", startIndex, length);
                if (endIndex == -1)
                {
                    endIndex = url.Length;
                }

                return url.Substring(startIndex, endIndex - startIndex);
            }
            else if (url.Contains("youtube.com/watch?list="))
            {
                int startIndex = url.IndexOf("list=") + 5;
                int length = url.Length - startIndex;
                int endIndex = url.IndexOf("&", startIndex, length);
                if (endIndex == -1)
                {
                    endIndex = url.Length;
                }

                return url.Substring(startIndex, endIndex - startIndex);
            }
            else if (url.Contains("youtu.be"))
            {
                int startIndex = url.LastIndexOf("/") + 1;
                return url.Substring(startIndex);
            }

            return null;
        }

    }
}
