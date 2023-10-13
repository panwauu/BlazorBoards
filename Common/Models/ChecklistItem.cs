using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Common.Models
{
    public class ChecklistItem : INotifyPropertyChanged
    {
        [Required]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        private bool _IsDone;
        private string _Title;

        [Required]
        public bool IsDone { get { return _IsDone; } set { if (_IsDone != value) { _IsDone = value; OnPropertyChanged(nameof(IsDone)); } } }

        [Required]
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

        public ChecklistItem(string Id, string title)
        {
            this.Id = Id;
            _Title = title;
            _IsDone = false;
        }

        [JsonConstructor]
        public ChecklistItem(string Id, string Title, bool IsDone)
        {
            this.Id = Id;
            _Title = Title;
            _IsDone = IsDone;
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
