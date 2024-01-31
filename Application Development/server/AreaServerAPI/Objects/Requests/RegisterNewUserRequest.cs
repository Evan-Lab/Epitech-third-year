
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

public class RegisterNewUserRequest
{
    [Required]
    public string FirstName { get; set; } = string.Empty;

    [Required]
    public string LastName { get; set; } = string.Empty;
    
    [Required]
    public string Email { get; set; } = string.Empty;

    [AllowNull]
    public string PhotoUrl {  get; set; } = string.Empty;

    [Required]
    public string Password { get; set; } = string.Empty;
}
