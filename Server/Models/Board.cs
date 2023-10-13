using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Models;

[Index(nameof(BlazorBoardId), nameof(Order), IsUnique = true)]
public class Board
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public Guid Id { get; set; }

    public Guid BlazorBoardId { get; set; }

    [Required]
    public int Order { get; set; }

    [Required]
    [MaxLength(100)]
    public string Title { get; set; } = string.Empty;

    [Required]
    public ICollection<Task>? Tasks { get; set; }
}
