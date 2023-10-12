using Microsoft.EntityFrameworkCore;
using Server.Models;

namespace Server.Data;

public class BoardContext : DbContext
{
    public BoardContext(DbContextOptions<BoardContext> options)
        : base(options)
    {
    }

    public DbSet<Label> Labels => Set<Label>();
}
