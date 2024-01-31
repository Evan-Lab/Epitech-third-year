using SpotifyAPI.Web;
using SpotifyAPI.Web.Auth;
using Swan;
using System;
using System.Collections.Generic;
using System.Net;
using System.Runtime.CompilerServices;
using System.Xml.Schema;

public class SpotifyAction
{
    public SpotifyAction() {}

    public async Task<string> HandleFirstRequest(string access_token, string playlistUrl, string requestName)
    {
        bool IsError = true;
        string total = "";
        int count = -1;
        try {
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", access_token);
            HttpResponseMessage response = null;
            if (requestName == "follower")
                response = await client.GetAsync($"https://api.spotify.com/v1/me");
            else if (requestName == "track") {
                string playlistId = DecryptUrl(playlistUrl);
                response = await client.GetAsync($"https://api.spotify.com/v1/playlists/{playlistId}/tracks");
            } else if (requestName == "playlist")
                response = await client.GetAsync($"https://api.spotify.com/v1/me/playlists");
            else {
                Console.WriteLine("Error: requestName not found");
                IsError = true;
            }
            if (response.IsSuccessStatusCode) {
                var responseData = await response.Content.ReadAsStringAsync();
                dynamic data = Newtonsoft.Json.JsonConvert.DeserializeObject(responseData);
                if (data == null) {
                    Console.WriteLine("Error: Request response is null");
                    IsError = true;
                }
                if (requestName == "follower") {
                    total = data.followers.total.ToString();
                    if (int.TryParse(total, out count) == false) {
                        Console.WriteLine("Error: Can't parse follower count");
                        IsError = true;
                    }
                } else if (requestName == "track") {
                    Console.WriteLine("Track count: " + data.total);
                    total = data.total.ToString();
                    if (int.TryParse(total, out count) == false) {
                        Console.WriteLine("Error: Can't parse track count");
                        IsError = true;
                    }
                } else if (requestName == "playlist") {
                    total = data.total.ToString();
                    if (int.TryParse(total, out count) == false) {
                        Console.WriteLine("Error: Can't parse playlist count");
                        IsError = true;
                    }
                } else {
                    Console.WriteLine("Error: requestName not found");
                    IsError = true;
                }

            } else {
                Console.WriteLine("Error: " + response.StatusCode + " Bad request");
                Console.WriteLine("Response content: " + await response.Content.ReadAsStringAsync());
                IsError = true;
            }
        } catch (Exception e) {
            Console.WriteLine("Error try: " + e.Message);
        }
        IsError = false;
        var result = new { error = IsError, value = new { comparator = count} };
        return Newtonsoft.Json.JsonConvert.SerializeObject(result);
    }

    public async Task<string> HandleNewFollower(string access_token, string count)
    {
        bool isError = true;
        try {
            if (int.TryParse(count, out int followerCount) == false) {
                Console.WriteLine("Error: Can't parse follower count");
                isError = true;
            }
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", access_token);
            HttpResponseMessage response = await client.GetAsync($"https://api.spotify.com/v1/me");

            if (response.IsSuccessStatusCode) {
                var responseData = await response.Content.ReadAsStringAsync();
                dynamic userData = Newtonsoft.Json.JsonConvert.DeserializeObject(responseData);
                if (userData == null) {
                    Console.WriteLine("Error: Request response is null");
                    isError = true;
                }
                string total = userData.followers.total.ToString();
                if (int.TryParse(total, out int nextFollowerCount) == false) {
                    Console.WriteLine("Error: Can't parse follower count");
                    isError = true;
                }
                Console.WriteLine("Follower count: " + followerCount);
                Console.WriteLine("New follower count: " + nextFollowerCount);
                if (followerCount < nextFollowerCount) {
                    Console.WriteLine("New follower added");
                    isError = false;
                } else {
                    Console.WriteLine("Next follower count is not greater than follower count");
                    isError = true;
                }
            } else {
                Console.WriteLine("Error: " + response.StatusCode + " Bad request. Response: " + response.ToString());
                Console.WriteLine("Response content: ", await response.Content.ReadAsStringAsync());
                isError = true;
            }
        } catch (Exception e) {
            Console.WriteLine("Error try: " + e.Message);
            isError = true;
        }
        var result = new { error = isError, value = new { } };
        return Newtonsoft.Json.JsonConvert.SerializeObject(result);
    }

    public string DecryptUrl(string playlistUrl)
    {
        string playlistId = "";

        if (playlistUrl.Contains("https://open.spotify.com/playlist/")) {
            playlistId = playlistUrl.Replace("https://open.spotify.com/playlist/", "");
            Console.WriteLine("Playlist id: " + playlistId);
        } else
            Console.WriteLine("Error: playlistUrl is not valid");
        return playlistId;
    }

    public async Task<string> HandleNewTrack(string access_token, string playlistUrl, string count)
    {
        bool isError = true;
        try {
            string playlistId = DecryptUrl(playlistUrl);
            if (int.TryParse(count, out int trackCount) == false) {
                Console.WriteLine("Error: Can't parse follower count");
                isError = true;
            }
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", access_token);
            HttpResponseMessage response = await client.GetAsync($"https://api.spotify.com/v1/playlists/{playlistId}/tracks");

            if (response.IsSuccessStatusCode) {
                int nextTrackCount = trackCount;
                var responseData = await response.Content.ReadAsStringAsync();
                dynamic trackData = Newtonsoft.Json.JsonConvert.DeserializeObject(responseData);
                nextTrackCount = trackData.total;
                Console.WriteLine("Track count: " + trackCount);
                Console.WriteLine("New track count: " + nextTrackCount);
                if (trackCount < nextTrackCount) {
                    Console.WriteLine("New track added");
                    isError = false;
                } else {
                    Console.WriteLine("Next track count is not greater than track count");
                    isError = true;
                }
            } else {
                Console.WriteLine("Error: " + response.StatusCode + " Bad request. Response: " + response.ToString());
                Console.WriteLine("Response content: ", await response.Content.ReadAsStringAsync());
                isError = true;
            }
        } catch (Exception e) {
            Console.WriteLine("Error try: " + e.Message);
            isError = true;
        }
        var result = new { error = isError, value = new { } };
        return Newtonsoft.Json.JsonConvert.SerializeObject(result);
    }

    public async Task<string> HandleNewPlaylist(string access_token, string count)
    {
        bool isError = true;
        try {
            if (int.TryParse(count, out int playlistCount) == false) {
                Console.WriteLine("Error: Can't parse follower count");
                isError = true;
            }
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", access_token);
            HttpResponseMessage response = await client.GetAsync($"https://api.spotify.com/v1/me/playlists");

            if (response.IsSuccessStatusCode) {
                int nextPlaylistCount = playlistCount;
                var responseData = await response.Content.ReadAsStringAsync();
                dynamic playlistData = Newtonsoft.Json.JsonConvert.DeserializeObject(responseData);
                nextPlaylistCount = playlistData.total;
                Console.WriteLine("Playlist count: " + playlistCount);
                Console.WriteLine("New playlist count: " + nextPlaylistCount);
                if (playlistCount < nextPlaylistCount) {
                    Console.WriteLine("New playlist added");
                    isError = false;
                } else {
                    Console.WriteLine("Next playlist count is not greater than playlist count");
                    isError = true;
                }
            } else {
                Console.WriteLine("Error: " + response.StatusCode + " Bad request. Response: " + response.ToString());
                Console.WriteLine("Response content: ", await response.Content.ReadAsStringAsync());
                isError = true;
            }
        } catch (Exception e) {
            Console.WriteLine("Error try: " + e.Message);
            isError = true;
        }
        var result = new { error = isError, value = new { } };
        return Newtonsoft.Json.JsonConvert.SerializeObject(result);
    }

    public async Task<bool> HandleSpotifyAction(string access_token, string playlistId, int actionID)
    {
        // bool isFinished = true;

        // if (actionID == 1)
        //     return await HandleNewTrack(access_token, playlistId, 
        // else if (actionID == 2)
        //     return await HandleNewPlaylist(access_token);
        // else if (actionID == 3)
        //     return await HandleNewFollower(access_token);
        // else {
        //     Console.WriteLine("Error: actionID not found");
        //     return false;
        // }
        // return isFinished;
        return true;
    }
}
