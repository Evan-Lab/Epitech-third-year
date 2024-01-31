using System.ComponentModel.DataAnnotations;

namespace AreaServerAPI.Objects.Requests
{
    public class DeleteUserIdRequest
    {
        [Required]
        public int IdUser { get; set; }
    }
}