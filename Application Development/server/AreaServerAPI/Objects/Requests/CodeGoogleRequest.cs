using System.ComponentModel.DataAnnotations;

namespace AreaServerAPI.Objects.Requests
{
    public class CodeGoogleRequest
    {
        [Required]
        public string Code { get; set; } = string.Empty;

        [Required]
        public string PageName { get; set; } = string.Empty;
    }
}