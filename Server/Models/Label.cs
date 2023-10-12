using System.ComponentModel.DataAnnotations;

namespace Server.Models;

public class LabelDB
{
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string? Name { get; set; }

    [Required]
    [MaxLength(7)]
    public string? Color { get; set; }

    [Required]
    [MaxLength(7)]
    public string? Background { get; set; }
}

