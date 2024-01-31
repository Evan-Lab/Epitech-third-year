using System.Net.Http.Headers;
using System.Text;

namespace AreaServer.Core.Class
{
    public class SpotifyReaction
    {
        private string _accessToken;

        public SpotifyReaction(string accessToken) 
        {
            _accessToken = accessToken;
        }

        public async Task<bool> FollowPlaylist(string url)
        {
            try
            {
                var getId = new SpotifyGetId();
                var playlistId = getId.GetIdFromUrl(url);
                if (playlistId == null)
                {
                    return false;
                }

                Console.WriteLine($"Playlist ID: {playlistId}");

                using var client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);
                HttpResponseMessage response = null;

                response = await client.PutAsync($"https://api.spotify.com/v1/playlists/{playlistId}/followers", null);

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Playlist suivie avec succès !");
                }
                else
                {
                    Console.WriteLine($"Erreur lors de la tentative de suivi de la playlist. Status code: {response}");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }

            return true;
        }

        public async Task<bool> FollowArtist(string url)
        {
            try
            {
                var getId = new SpotifyGetId();
                var artistId = getId.GetIdFromUrl(url);
                if (artistId == null)
                {
                    return false;
                }

                Console.WriteLine($"Artist ID: {artistId}");

                using var client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);
                HttpResponseMessage response = null;

                response = await client.PutAsync($"https://api.spotify.com/v1/me/following?type=artist&ids={artistId}", null);
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Artiste suivi avec succès !");
                }
                else
                {
                    Console.WriteLine($"Erreur lors de la tentative de suivi de l'artiste. Error: {response}");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }

            return true;
        }

        public async Task<bool> AddTrackInPlaylist(string urlPlaylist, string urlTrack)
        {
            try
            {
                var getId = new SpotifyGetId();
                var playlistId = getId.GetIdFromUrl(urlPlaylist);
                var trackId = getId.GetIdFromUrl(urlTrack);
                Console.WriteLine($"Track ID: {trackId}, Playlist: {playlistId}");
                if (trackId == null || playlistId == null)
                { 
                    return false;
                }

                using var client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);
                HttpResponseMessage response = null;


                var content = new StringContent("", Encoding.UTF8, "application/json");
                response = await client.PostAsync($"https://api.spotify.com/v1/playlists/{playlistId}/tracks?uris=spotify:track:{trackId}", content);
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Musique ajoutée à la playlist avec succès !");
                }
                else
                {
                    Console.WriteLine($"Erreur lors de la tentative d'ajout de la musique à la playlist. Error: {response}");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }

            return true;
        }
    }
}
