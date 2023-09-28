public class TodoItem
{
    public required string Title { get; set; }
    public bool IsDone { get; set; }

    public override bool Equals(object? obj)
    {
        if (obj == null || GetType() != obj.GetType()) return false;
        var compare = (TodoItem)obj;
        return Title.Equals(compare.Title) && IsDone.Equals(compare.IsDone);
    }
}
