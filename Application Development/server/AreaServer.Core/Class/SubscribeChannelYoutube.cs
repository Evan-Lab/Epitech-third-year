using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;
using Newtonsoft.Json;
using System.Threading.Channels;

namespace AreaServer.Core.Class
{
    public class SubscribeChannelYoutube
    {
        public SubscribeChannelYoutube() { }

        public async Task<bool> SubscribeChannel(string url, string accessToken)
        {
            try {
                var getID = new GetYoutubeID();
                string channelId = getID.GetChannelIdFromUsername(url, accessToken);

                var credentials = GoogleCredential.FromAccessToken(accessToken).CreateScoped(YouTubeService.Scope.YoutubeForceSsl);
                var youtubeService = new YouTubeService(new BaseClientService.Initializer()
                {
                    HttpClientInitializer = credentials,
                    ApplicationName = "YouTube Subscriber Checker"
                });

                Console.WriteLine($"CHANNEL ID {channelId}");
                Subscription subscription = new Subscription
                {
                    Snippet = new SubscriptionSnippet
                    {
                        ResourceId = new ResourceId
                        {
                            Kind = "youtube#channel",
                            ChannelId = channelId
                        }
                    }
                };

                youtubeService.Subscriptions.Insert(subscription, "snippet").Execute();
                Console.WriteLine("Vous êtes désormais abonné à la chaîne avec l'ID : " + channelId);
            } catch (Exception e) {
                Console.WriteLine($"Error: {e}");
                return false;
            }
            return true;
        }
    }
}
