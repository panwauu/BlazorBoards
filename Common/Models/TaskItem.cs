namespace Common.Models
{
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
}
