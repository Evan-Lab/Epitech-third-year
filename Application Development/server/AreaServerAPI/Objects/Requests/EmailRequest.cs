using System.ComponentModel.DataAnnotations;

namespace AreaServerAPI.Objects.Requests
{
    public class EmailRequest
    {
        [Required]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string City { get; set; } = string.Empty;

        [Required]
        public int Hour { get; set; } = 0;

        [Required]
        public int Minute { get; set; } = 0;
    }
}