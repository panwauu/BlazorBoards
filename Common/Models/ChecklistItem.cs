namespace Common.Models
{
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
}
