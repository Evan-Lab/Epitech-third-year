using System.ComponentModel.DataAnnotations;

namespace AreaServerAPI.Objects.Requests
{
    public class CodeDeezerRequest
    {
        [Required]
        public string Code { get; set; } = string.Empty;
    }
}