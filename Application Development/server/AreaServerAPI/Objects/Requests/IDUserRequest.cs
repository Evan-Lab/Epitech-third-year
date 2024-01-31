using System.ComponentModel.DataAnnotations;

namespace AreaServerAPI.Objects.Requests
{
    public class IdUserRequest
    {
        [Required]
        public int IdUser { get; set; }
    }
}