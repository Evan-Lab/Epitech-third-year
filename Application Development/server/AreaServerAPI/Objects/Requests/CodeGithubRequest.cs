using System.ComponentModel.DataAnnotations;

namespace AreaServerAPI.Objects.Requests
{
    public class CodeGithubRequest
    {
        [Required]
        public string Code { get; set; } = string.Empty;
    }
}