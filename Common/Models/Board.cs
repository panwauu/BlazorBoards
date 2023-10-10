namespace Common.Models
{
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
}
