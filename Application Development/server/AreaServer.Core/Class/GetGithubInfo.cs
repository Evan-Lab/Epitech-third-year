using System;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Octokit;
using System.ComponentModel;
using System.Diagnostics;

public class GithubAction
{
    public GithubAction() { }

    public class GithubEvent
    {
        public string type { get; set; }
        public GithubActor actor { get; set; }
        public DateTime created_at { get; set; }
        public GithubPayload payload { get; set; }
        public GithubRepo repo { get; set; }
    }

    public class GithubPayload
    {
        public string action { get; set; }
        public string ref_type { get; set; }
    }

    public class GithubRepo
    {
        public string name { get; set; }
    }

    public class GithubActor
    {
        public string login { get; set; }
    }

    public async Task<string> GetUsername(string access_token)
    {
        using (var client = new HttpClient()) {
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {access_token}");
            client.DefaultRequestHeaders.Add("User-Agent", "CraftArea_Web");

            var apiUrl = "https://api.github.com/user";

            var response = await client.GetAsync(apiUrl);
            if (response.IsSuccessStatusCode) {
                var userJson = await response.Content.ReadAsStringAsync();
                dynamic userData = JsonConvert.DeserializeObject(userJson);
                string username = userData.login;
                return username;
            } else {
                Console.WriteLine("Error: " + response.StatusCode + " Bad request");
            }
        }
        return "";
    }
    public async Task<string> HandleGithubAction(string access_token, string repoName, DateTime programStartTime, int actionID)
    {
        bool isError = true;
        var apiUrl = "";
        string repoOwner = GetUsername(access_token).Result;
        Console.WriteLine("Repo Owner: ", GetUsername(access_token).Result);
        if (actionID == 1 || actionID == 3 || actionID == 4 || actionID == 5 || actionID == 6 || actionID == 7)
            apiUrl = $"https://api.github.com/repos/{repoOwner}/{repoName}/events";
        else if (actionID == 2)
            apiUrl = $"https://api.github.com/users/{repoOwner}/events";
        else
            isError = true;
        Console.WriteLine("API URL: " + apiUrl);
        if (repoOwner == "")
            isError = true;
        Console.WriteLine("Repo Owner: " + repoOwner);
        try {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {access_token}");
                client.DefaultRequestHeaders.Add("User-Agent", "CraftArea_Web");

                var response = await client.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    var eventsJson = await response.Content.ReadAsStringAsync();
                    var events = JsonConvert.DeserializeObject<List<GithubEvent>>(eventsJson);

                    foreach (var e in events)
                    {
                        Console.WriteLine("Event type: " + e.type);
                        if (actionID == 1)
                            if (e.type == "PushEvent" && e.created_at >= programStartTime) {
                                Console.WriteLine($"Push detected on {repoOwner}/{repoName} at {e.created_at} by {e.actor.login}");
                                isError = false;
                            }
                        if (actionID == 2)
                            if (e.type == "CreateEvent" && e.created_at >= programStartTime) {
                                Console.WriteLine($"Repository created by {e.actor.login} at {e.created_at}");
                                isError = false;
                            }
                        if (actionID == 3) 
                            if (e.type == "PullRequestEvent" && e.payload.action == "opened" && e.repo.name == $"{repoOwner}/{repoName}" && e.created_at >= programStartTime) {
                                Console.WriteLine($"Pull Request created by {repoOwner} in {e.repo.name} at {e.created_at}");
                                isError = false;
                            }
                        if (actionID == 4)
                            if (e.type == "PullRequestEvent" && e.payload.action == "closed" && e.repo.name == $"{repoOwner}/{repoName}" && e.created_at >= programStartTime) {
                                Console.WriteLine($"Pull Request closed by {repoOwner} in {e.repo.name} at {e.created_at}");
                                isError = false;
                            }
                        if (actionID == 5)
                            if (e.type == "DeleteEvent" && e.payload.ref_type == "branch" && e.created_at >= programStartTime) {
                                Console.WriteLine($"Branch deleted in {e.repo.name} at {e.created_at}");
                                isError = false;
                            }
                        if (actionID == 6)
                            if (e.type == "MemberEvent" && e.payload.action == "added" && e.repo.name == $"{repoOwner}/{repoName}" && e.created_at >= programStartTime) {
                                Console.WriteLine($"New collaborator added to {e.repo.name} at {e.created_at} by {e.actor.login}");
                                isError = false;
                            }
                        if (actionID == 7)
                            if (e.type == "CreateEvent" && e.payload.ref_type == "branch" && e.created_at >= programStartTime) {
                                Console.WriteLine($"Branch created in {e.repo.name} at {e.created_at}");
                                isError = false;
                            }
                    }
                } else {
                    Console.WriteLine("Error: " + response.StatusCode + " Bad request");
                    isError = true;
                }
            }
        } catch (Exception ex) {
            Console.WriteLine($"Error: {ex.Message}");
            isError = true;
        }
        var result = new { error = isError, value = new { } };
        return JsonConvert.SerializeObject(result);
    }
}