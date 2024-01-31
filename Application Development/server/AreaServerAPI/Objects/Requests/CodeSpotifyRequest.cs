using System.ComponentModel.DataAnnotations;

namespace AreaServerAPI.Objects.Requests
{
    public class CodeSpotifyRequest
    {
        [Required]
        public string Code { get; set; } = string.Empty;
    }
}