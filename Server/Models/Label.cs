using System.ComponentModel.DataAnnotations;

namespace Server.Models;

public class Label
{
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string? Name { get; set; }

    [Required]
    [MaxLength(7)]
    public string? Color;

    [Required]
    [MaxLength(7)]
    public string? Background;
}

