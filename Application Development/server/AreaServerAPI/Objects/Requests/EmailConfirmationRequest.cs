using System.ComponentModel.DataAnnotations;

namespace AreaServerAPI.Objects.Requests
{
    public class EmailConfirmationRequest
    {
        [Required]
        public bool Set { get; set; } = false;

        [Required]
        public string Email { get; set; } = string.Empty;
    }
}