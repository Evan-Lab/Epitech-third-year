using Google.Apis.YouTube.v3.Data;
using Google.Apis.YouTube.v3;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;

namespace AreaServer.Core.Class
{
    public class AddVideoInPLaylistYoutubeReaction
    {
        public AddVideoInPLaylistYoutubeReaction() { }

        public async Task<bool> AddVideoInPlaylist(string urlVideo, string urlPlaylist, string accessToken)
        {
            try
            {
                var infoIdYoutube = new GetYoutubeID();
                var playlistId = infoIdYoutube.GetPlaylistId(urlPlaylist);
                var videoId = infoIdYoutube.GetVideoId(urlVideo);

                var credentials = GoogleCredential.FromAccessToken(accessToken).CreateScoped(YouTubeService.Scope.YoutubeForceSsl);
                var youtubeService = new YouTubeService(new BaseClientService.Initializer()
                {
                    HttpClientInitializer = credentials,
                    ApplicationName = "YouTube Subscriber Checker"
                });

                var playlistItem = new PlaylistItem();
                playlistItem.Snippet = new PlaylistItemSnippet();
                playlistItem.Snippet.PlaylistId = playlistId;
                playlistItem.Snippet.ResourceId = new ResourceId { Kind = "youtube#video", VideoId = videoId };

                var request = youtubeService.PlaylistItems.Insert(playlistItem, "snippet");
                var response = request.Execute();

                if (response == null)
                {
                    Console.WriteLine("Erreur lors de l'ajout de la vidéo à la playlist.");
                    return false;
                }
            } catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }

            return true;
        }
    }
}
