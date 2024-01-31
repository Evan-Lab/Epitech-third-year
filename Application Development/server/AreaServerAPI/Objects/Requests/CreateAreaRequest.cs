
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

public class CreateAreaRequest
{
    [Required]
    public string Name { get; set; } = string.Empty;

    [Required]
    public int UserId { get; set; }

    [Required]
    public int ActionId { get; set; }

    [Required]
    public int ReactionId { get; set; }

    public string ParamAction {  get; set; } = string.Empty;

    public string ParamReaction {  get; set; } = string.Empty;

}
