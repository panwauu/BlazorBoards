using System.ComponentModel;

namespace Common.Models
{
    public class ChecklistItem : INotifyPropertyChanged
    {
        public readonly string Id = Guid.NewGuid().ToString();

        private bool _IsDone;
        private string _Title;

        public bool IsDone { get { return _IsDone; } set { if (_IsDone != value) { _IsDone = value; OnPropertyChanged(nameof(IsDone)); } } }
        public string Title { get { return _Title; } set { if (_Title != value) { _Title = value; OnPropertyChanged(nameof(Title)); } } }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ChecklistItem(string title)
        {
            _Title = title;
            _IsDone = false;
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
