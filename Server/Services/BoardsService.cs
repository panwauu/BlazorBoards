using Microsoft.EntityFrameworkCore;
using Server.Data;
using Server.Models;

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

    public void Put(Common.Models.BlazorBoardData blazorBoardData)
    {
        // Sync boards
        var dbBlazorBoard = _context.BlazorBoards
            .Include("Boards.Tasks.Labels")
            .Include("Boards.Tasks.Checklist")
            .Include("Labels")
            .First();

        // Labels update: Delete all old labels, then add new ones or update existing ones
        dbBlazorBoard.Labels ??= new List<Label>();

        var idsToRemove = new List<string>();
        idsToRemove = dbBlazorBoard.Labels.Select(l => l.Id.ToString()).Except(blazorBoardData.Labels.Select(l => l.Id)).ToList();
        dbBlazorBoard.Labels.Where(l => idsToRemove.Contains(l.Id.ToString())).ToList().ForEach(l => dbBlazorBoard.Labels.Remove(l));

        foreach (var newlabel in blazorBoardData.Labels)
        {
            var dbLabel = dbBlazorBoard.Labels.FirstOrDefault(x => x.Id.ToString() == newlabel.Id);
            if (dbLabel == null)
            {
                dbBlazorBoard.Labels.Add(new Label() { Id = Guid.Parse(newlabel.Id), Name = newlabel.Title, Color = newlabel.Color, Background = newlabel.Background });
            }
            else
            {
                dbLabel.Name = newlabel.Title;
                dbLabel.Color = newlabel.Color;
                dbLabel.Background = newlabel.Background;
            }
        }

        // Boards update: Delete all old boards, then add new ones or update existing ones
        dbBlazorBoard.Boards ??= new List<Board>();

        var boardIdsToRemove = dbBlazorBoard.Boards.Select(b => b.Id.ToString()).Except(blazorBoardData.Boards.Select(b => b.Id)).ToList();
        dbBlazorBoard.Boards.Where(b => idsToRemove.Contains(b.Id.ToString())).ToList().ForEach(b => dbBlazorBoard.Boards.Remove(b));

        foreach (var updatedBoard in blazorBoardData.Boards)
        {
            if (updatedBoard == null) continue;
            var dbBoard = dbBlazorBoard.Boards.FirstOrDefault(x => x.Id.ToString() == updatedBoard.Id);
            if (dbBoard == null)
            {
                dbBoard = new Board() { Id = Guid.Parse(updatedBoard.Id), Title = updatedBoard.Title, Tasks = new List<Models.Task>() };
                foreach (var task in updatedBoard.Tasks)
                {
                    if (task == null) continue;
                    var dbTask = new Models.Task() { Id = Guid.Parse(task.Id), Title = task.Title, Description = task.Description, Labels = new List<Label>(), Checklist = new List<Checklist>() };
                    foreach (var label in task.Labels)
                    {
                        if (label == null) continue;
                        var dbLabel = dbBlazorBoard.Labels.FirstOrDefault(x => x.Id.ToString() == label);
                        if (dbLabel != null) dbTask.Labels.Add(dbLabel);
                    }
                    foreach (var checklistitem in task.Checklist)
                    {
                        if (checklistitem == null) continue;
                        var dbChecklistitem = new Checklist() { Id = Guid.Parse(checklistitem.Id), Title = checklistitem.Title, IsDone = checklistitem.IsDone };
                        dbTask.Checklist.Add(dbChecklistitem);
                    }
                    dbBoard.Tasks.Add(dbTask);
                }
                dbBlazorBoard.Boards.Add(dbBoard);
            }
            else
            {
                dbBoard.Tasks ??= new List<Models.Task>();
                dbBoard.Title = updatedBoard.Title;
                var tasksToRemove = dbBoard.Tasks.Select(t => t.Id.ToString()).Except(updatedBoard.Tasks.Select(t => t.Id)).ToList();
                dbBoard.Tasks.Where(t => tasksToRemove.Contains(t.Id.ToString())).ToList().ForEach(t => dbBoard.Tasks.Remove(t));

                foreach (var updatedTask in updatedBoard.Tasks)
                {
                    var task = dbBoard.Tasks.FirstOrDefault(x => x.Id.ToString() == updatedTask.Id);
                    if (task == null)
                    {                   
                        var newTask = new Models.Task() { Id = Guid.Parse(updatedTask.Id), Title = updatedTask.Title, Description = updatedTask.Description, Labels = new List<Label>(), Checklist = new List<Checklist>() };
                        foreach (var label in updatedTask.Labels)
                        {
                            if (label == null) continue;
                            var dbLabel = dbBlazorBoard.Labels.FirstOrDefault(x => x.Id.ToString() == label);
                            if (dbLabel != null) newTask.Labels.Add(dbLabel);
                        }
                        foreach (var checklist in updatedTask.Checklist)
                        {
                            if (checklist == null) continue;
                            var dbChecklist = new Checklist() { Id = Guid.Parse(checklist.Id), Title = checklist.Title, IsDone = checklist.IsDone };
                            newTask.Checklist.Add(dbChecklist);
                        }
                        dbBoard.Tasks.Add(newTask);
                    }
                    else
                    {
                        task.Labels ??= new List<Label>();
                        task.Checklist ??= new List<Checklist>();
                        task.Title = updatedTask.Title;
                        task.Description = updatedTask.Description;
                        task.Deadline = updatedTask.Deadline;

                        task.Labels.Clear();
                        foreach (var label in updatedTask.Labels)
                        {
                            if (label == null) continue;
                            var dbLabel = dbBlazorBoard.Labels.FirstOrDefault(x => x.Id.ToString() == label);
                            if (dbLabel != null) task.Labels.Add(dbLabel);
                        }
                        task.Checklist.Clear();
                        foreach (var checklist in updatedTask.Checklist)
                        {
                            if (checklist == null) continue;
                            var dbChecklist = new Checklist() { Id = Guid.Parse(checklist.Id), Title = checklist.Title, IsDone = checklist.IsDone };
                            task.Checklist.Add(dbChecklist);
                        }
                    }
                }
            }
        }

        _context.SaveChanges();
    }
}

