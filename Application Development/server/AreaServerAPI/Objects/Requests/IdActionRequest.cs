using System.ComponentModel.DataAnnotations;

namespace AreaServerAPI.Objects.Requests
{
    public class IdActionRequest
    {
        [Required]
        public int IdAction { get; set; }
    }
}