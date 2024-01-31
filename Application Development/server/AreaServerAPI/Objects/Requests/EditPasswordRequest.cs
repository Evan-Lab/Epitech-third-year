using System.ComponentModel.DataAnnotations;

public class EditPasswordRequest
{
    [Required]
    public int Id { get; set; }
    [Required]
    public string Password { get; set; } = string.Empty;
}