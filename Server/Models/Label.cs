using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Models;

public class Label
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public Guid Id { get; set; }

    public Guid BlazorBoardId { get; set; }

    [Required]
    public int Order { get; set; }

    public ICollection<Task>? Tasks { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [MaxLength(7)]
    public string Color { get; set; } = string.Empty;

    [Required]
    [MaxLength(7)]
    public string Background { get; set; } = string.Empty;
}
