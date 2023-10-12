using System.ComponentModel;

namespace Common.Models
{
    public class Board : INotifyPropertyChanged
    {
        public readonly string Id = Guid.NewGuid().ToString();

        private string _Title;
        private ObservableList<TaskItem> _Tasks;

        public string Title { get { return _Title; } set { if (_Title != value) { _Title = value; OnPropertyChanged(nameof(Title)); } } }
        public ObservableList<TaskItem> Tasks
        {
            get { return _Tasks; }
            set { if (_Tasks != value) { _Tasks = value; _Tasks.PropertyChanged += (sender, e) => OnPropertyChanged(nameof(Tasks)); OnPropertyChanged(nameof(Tasks)); } }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public Board(string title)
        {
            _Title = title;
            _Tasks = new ObservableList<TaskItem>();
            _Tasks.PropertyChanged += (sender, e) => OnPropertyChanged(nameof(Tasks));
        }

        public Board(string Id, string Title, ObservableList<TaskItem> Tasks)
        {
            this.Id = Id;
            _Title = Title;
            _Tasks = Tasks;
            _Tasks.PropertyChanged += (sender, e) => OnPropertyChanged(nameof(Tasks));
        }
    }
}
