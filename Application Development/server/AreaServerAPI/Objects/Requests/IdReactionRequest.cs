using System.ComponentModel.DataAnnotations;

namespace AreaServerAPI.Objects.Requests
{
    public class IdReactionRequest
    {
        [Required]
        public int IdReaction { get; set; }
    }
}