using System.ComponentModel.DataAnnotations;

public class WorldTimeRequest
{
    [Required]
    public int Day { get; set; } = -1;

    [Required]
    public int Month { get; set; } = -1;

    [Required]
    public int Year { get; set; } = -1;

    [Required]
    public int Hour { get; set; } = -1;

    [Required]
    public int Minute { get; set; } = -1;
}
