using Server.Models;

namespace Server.Data;

public static class DbInitializer
{
    public static void Initialize(BoardContext context)
    {
        if (context.BlazorBoards.Any()) { return; }

        var labelPrio1 = new Label { Id = Guid.NewGuid(), Name = "Prio high", Color = "#000000", Background = "#ff0000", Order = 0 };
        var labelPrio2 = new Label { Id = Guid.NewGuid(), Name = "Prio low", Color = "#000000", Background = "#00ff00", Order = 1 };
        var labelSeverity1 = new Label { Id = Guid.NewGuid(), Name = "Severity high", Color = "#000000", Background = "#ff0000", Order = 2 };
        var labelSeverity2 = new Label { Id = Guid.NewGuid(), Name = "Severity low", Color = "#000000", Background = "#00ff00", Order = 3 };

        var checklist1 = new Checklist { Id = Guid.NewGuid(), Title = "Checklist 1", IsDone = true, Order = 0 };
        var checklist2 = new Checklist { Id = Guid.NewGuid(), Title = "Checklist 2", IsDone = false, Order = 1 };

        var board1 = new Board
        {
            Id = Guid.NewGuid(),
            Order = 0,
            Title = "In progress",
            Tasks = new List<Server.Models.Task> {
            new Server.Models.Task {
                Id = Guid.NewGuid(),
                Order = 0,
                Title = "Fix Bug1",
                Deadline = DateTime.Now,
                Labels = new List<Label> { labelPrio1, labelSeverity1 } },
            new Server.Models.Task {
                Id = Guid.NewGuid(),
                Order = 1,
                Title = "Fix Bug2",
                Checklist = new List<Checklist> { checklist1, checklist2 },
                Description = "What a f*** bug",
                Labels = new List<Label> { labelPrio2, labelSeverity1 } }
        }
        };

        context.BlazorBoards.Add(
            new BlazorBoard
            {
                Id = Guid.NewGuid(),
                Boards = new List<Board> { board1 },
                Labels = new List<Label> { labelPrio1, labelPrio2, labelSeverity1, labelSeverity2 }
            });
        context.SaveChanges();
    }
}