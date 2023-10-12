using Microsoft.EntityFrameworkCore;
using Server.Data;
using Server.Models;
using System.Collections.ObjectModel;

namespace Server.Services;

public class BoardsService
{
    private readonly BoardContext _context;

    public BoardsService(BoardContext context)
    {
        _context = context;
    }

    public Common.Models.BlazorBoardData? Get()
    {
        var dbBlazorBoard = _context.BlazorBoards
            .Include("Boards.Tasks.Labels")
            .Include("Boards.Tasks.Checklist")
            .Include("Labels")
            .AsNoTracking()
            .First();
        if (dbBlazorBoard == null) return null;

        Common.Models.BlazorBoardData blazorBoard = new();

        foreach (var board in dbBlazorBoard.Boards ?? Enumerable.Empty<Board>())
        {
            if (board == null) continue;
            var boardToAdd = new Common.Models.Board(Id: board.Id.ToString(), Title: board.Title, Tasks: new Common.Models.ObservableList<Common.Models.TaskItem>());
            foreach (var task in board.Tasks ?? Enumerable.Empty<Models.Task>())
            {
                if (task == null) continue;
                var taskToAdd = new Common.Models.TaskItem(Id: task.Id.ToString(), Title: task.Title)
                {
                    Description = task.Description,
                };
                foreach (var checklistitem in task.Checklist ?? Enumerable.Empty<Checklist>())
                {
                    if (checklistitem == null) continue;
                    taskToAdd.Checklist.Add(new Common.Models.ChecklistItem(checklistitem.Id.ToString(), checklistitem.Title) { IsDone = checklistitem.IsDone });
                }
                foreach (var label in task.Labels ?? Enumerable.Empty<Label>())
                {
                    if (label == null) continue;
                    taskToAdd.Labels.Add(label.Id.ToString());
                }
                boardToAdd.Tasks.Add(taskToAdd);
            }
            blazorBoard.Boards.Add(boardToAdd);
        }

        foreach (var label in dbBlazorBoard.Labels ?? Enumerable.Empty<Label>())
        {
            if (label == null) continue;
            blazorBoard.Labels.Add(new Common.Models.Label(label.Id.ToString(), label.Name, label.Color, label.Background));
        }

        return blazorBoard;
    }

    public BlazorBoard Set()
    {
        throw new NotImplementedException();
    }
}

