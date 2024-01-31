
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

public class NewAreaServiceRequest
{

    [Required]
    public string Name { get; set; } = string.Empty;

    [AllowNull]
    public string LogoUrl { get; set; } = string.Empty;

}
