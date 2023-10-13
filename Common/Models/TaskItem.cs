using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Common.Models
{
    public class TaskItem : INotifyPropertyChanged
    {
        [Required]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        private string _Title;
        private string? _Description;
        private DateTime? _Deadline;
        private ObservableCollection<string> _Labels;
        private ObservableList<ChecklistItem> _Checklist;

        [Required]
        public string Title { get { return _Title; } set { if (_Title != value) { _Title = value; OnPropertyChanged(nameof(Title)); } } }
        public string? Description { get { return _Description; } set { if (_Description != value) { _Description = value; OnPropertyChanged(nameof(Description)); } } }
        public DateTime? Deadline { get { return _Deadline; } set { if (_Deadline != value) { _Deadline = value; OnPropertyChanged(nameof(Deadline)); } } }
        public ObservableCollection<string> Labels
        {
            get { return _Labels; }
            set { if (_Labels != value) { _Labels = value; _Labels.CollectionChanged += (sender, e) => OnPropertyChanged(nameof(Labels)); OnPropertyChanged(nameof(Labels)); } }
        }
        public ObservableList<ChecklistItem> Checklist
        {
            get { return _Checklist; }
            set { if (_Checklist != value) { _Checklist = value; _Checklist.PropertyChanged += (sender, e) => OnPropertyChanged(nameof(Checklist)); OnPropertyChanged(nameof(Checklist)); } }
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
            _Checklist = new ObservableList<ChecklistItem>();
            _Checklist.PropertyChanged += (sender, e) => OnPropertyChanged(nameof(Checklist));
        }

        public TaskItem(string Id, string Title)
        {
            this.Id = Id;
            _Title = Title;
            _Labels = new ObservableCollection<string>();
            _Labels.CollectionChanged += (sender, e) => OnPropertyChanged(nameof(Labels));
            _Checklist = new ObservableList<ChecklistItem>();
            _Checklist.PropertyChanged += (sender, e) => OnPropertyChanged(nameof(Checklist));
        }

        [JsonConstructor]
        public TaskItem(string Id, string Title, string? Description, DateTime? Deadline, ObservableCollection<string> Labels, ObservableList<ChecklistItem> Checklist)
        {
            this.Id = Id;
            _Title = Title;
            _Description = Description;
            _Deadline = Deadline;
            _Labels = Labels;
            _Labels.CollectionChanged += (sender, e) => OnPropertyChanged(nameof(Labels));
            _Checklist = Checklist;
            _Checklist.PropertyChanged += (sender, e) => OnPropertyChanged(nameof(Checklist));
        }
    }
}
