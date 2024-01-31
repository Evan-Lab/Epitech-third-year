using System.ComponentModel.DataAnnotations;

namespace AreaServerAPI.Objects.Requests
{
    public class IdAreaRequest
    {
        [Required]
        public int IdAreaUser { get; set; }
    }
}