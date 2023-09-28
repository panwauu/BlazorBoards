public class ChecklistItem
{
    public bool IsDone { get; set; }
    public string Title { get; set; }

    public ChecklistItem(string title)
    {
        Title = title;
        IsDone = false;
    }

    public override bool Equals(object? obj)
    {
        if ((obj == null) || !this.GetType().Equals(obj.GetType())) return false;
     
        var itemToCompare = (ChecklistItem)obj;
        return (Title == itemToCompare.Title) && (IsDone == itemToCompare.IsDone);
    }
}

public class TaskItem
{
    public string Title { get; set; }
    public string? Description { get; set; }
    public DateTime? Deadline { get; set; }
    public List<string> Labels { get; set; }
    public List<ChecklistItem> Checklist { get; set; }

    public TaskItem(string title)
    {
        Title = title;
        Labels = new List<string>();
        Checklist = new List<ChecklistItem>();
    }
}

public class Board
{
    public string Title { get; set; }
    public List<TaskItem> Tasks { get; set; }

    public Board(string title)
    {
        Title = title;
        Tasks = new List<TaskItem>();
    }
}