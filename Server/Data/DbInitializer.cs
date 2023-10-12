using Server.Models;

namespace Server.Data;

public static class DbInitializer
{
    public static void Initialize(BoardContext context)
    {
        if (context.Labels.Any()) { return; }

        context.Labels.AddRange(new LabelDB[]
        {
            new() { Name = "prio: low", Color = "#000000", Background = "#aa2222" },
            new() { Name = "prio: high", Color = "#000000", Background = "#22aa22" },
        });

        context.SaveChanges();
    }
}