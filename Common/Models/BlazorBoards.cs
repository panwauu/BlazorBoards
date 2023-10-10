using System.ComponentModel;

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

    public override int GetHashCode()
    {
        return (Title + IsDone.ToString()).GetHashCode();
    }
}

public class TaskItem
{
    public string Id { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; }
    public DateTime? Deadline { get; set; }
    public List<Label> Labels { get; set; }
    public List<ChecklistItem> Checklist { get; set; }

    public TaskItem(string title)
    {
        Id = Guid.NewGuid().ToString();
        Title = title;
        Labels = new List<Label>();
        Checklist = new List<ChecklistItem>();
    }
}

public class Board
{
    public string Id { get; set; }
    public string Title { get; set; }
    public List<TaskItem> Tasks { get; set; }

    public Board(string title)
    {
        Id = Guid.NewGuid().ToString();
        Title = title;
        Tasks = new List<TaskItem>();
    }
}

public class Label : INotifyPropertyChanged
{
    public readonly string Id = Guid.NewGuid().ToString();
    private string _Title;
    private string _Color;
    private string _Background;

    public string Title { get { return _Title; } set { if (_Title != value) { _Title = value; OnPropertyChanged(nameof(Title)); } } }
    public string Color { get { return _Color; } set { if (_Color != value) { _Color = value; OnPropertyChanged(nameof(Color)); } } }
    public string Background { get { return _Background; } set { if (_Background != value) { _Background = value; OnPropertyChanged(nameof(Background)); } } }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }


    public Label(string title, string color, string background)
    {
        _Title = title;
        _Color = color;
        _Background = background;
        PropertyChanged = null;
    }
}