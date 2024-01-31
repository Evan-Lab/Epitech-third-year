
using System.ComponentModel.DataAnnotations;

namespace AreaServerAPI.Objects.Requests
{
    public class UriRedirectRequest
    {
        [Required]
        public string PageName { get; set; } = string.Empty;
    }
}