using System.ComponentModel.DataAnnotations;

namespace AreaServerAPI.Objects.Requests
{
    public class CodeFacebookRequest
    {
        [Required]
        public string Code { get; set; } = string.Empty;
    }
}