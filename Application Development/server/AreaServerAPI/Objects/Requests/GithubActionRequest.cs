using System.ComponentModel.DataAnnotations;

namespace AreaServerAPI.Objects.Requests
{
    public class GithubActionRequest
    {
        [Required]
        public string AccessToken { get; set; } = string.Empty;
        [Required]
        public string RepoName { get; set; } = string.Empty;
    }
}