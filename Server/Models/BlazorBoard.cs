using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Models;

public class BlazorBoard
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public Guid Id { get; set; }

    public ICollection<Board>? Boards { get; set; }

    public ICollection<Label> Labels { get; set; }
}
