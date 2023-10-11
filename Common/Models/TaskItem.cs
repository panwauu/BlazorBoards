using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Common.Models
{
    public class TaskItem : INotifyPropertyChanged
    {
        public readonly string Id = Guid.NewGuid().ToString();

        private string _Title;
        private string? _Description;
        private DateTime? _Deadline;
        private ObservableCollection<string> _Labels;
        private ObservableCollection<ChecklistItem> _Checklist;

        public string Title { get { return _Title; } set { if (_Title != value) { _Title = value; OnPropertyChanged(nameof(Title)); } } }
        public string? Description { get { return _Description; } set { if (_Description != value) { _Description = value; OnPropertyChanged(nameof(Description)); } } }
        public DateTime? Deadline { get { return _Deadline; } set { if (_Deadline != value) { _Deadline = value; OnPropertyChanged(nameof(Deadline)); } } }
        public ObservableCollection<string> Labels
        {
            get { return _Labels; }
            set { if (_Labels != value) { _Labels = value; _Labels.CollectionChanged += (sender, e) => OnPropertyChanged(nameof(Labels)); OnPropertyChanged(nameof(Labels)); } }
        }
        public ObservableCollection<ChecklistItem> Checklist
        {
            get { return _Checklist; }
            set { if (_Checklist != value) { _Checklist = value; _Checklist.CollectionChanged += (sender, e) => OnPropertyChanged(nameof(Checklist)); OnPropertyChanged(nameof(Checklist)); } }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public TaskItem(string title)
        {
            _Title = title;
            _Labels = new ObservableCollection<string>();
            _Labels.CollectionChanged += (sender, e) => OnPropertyChanged(nameof(Labels));
            _Checklist = new ObservableCollection<ChecklistItem>();
            _Checklist.CollectionChanged += (sender, e) => OnPropertyChanged(nameof(Checklist));
        }
    }
}
