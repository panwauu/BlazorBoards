using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Models;

public class Task
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public Guid Id { get; set; }

    public Guid BoardId { get; set; }

    [Required]
    public int Order { get; set; }

    [Required]
    [MaxLength(100)]
    public string Title { get; set; } = string.Empty;

    [MaxLength(10000)]
    public string? Description { get; set; }

    [DataType(DataType.Date)]
    public DateTime? Deadline { get; set; }

    public ICollection<Label>? Labels { get; set; }

    public ICollection<Checklist>? Checklist { get; set; }
}

