using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Common.Models
{
    public class Board : INotifyPropertyChanged
    {
        public readonly string Id = Guid.NewGuid().ToString();

        private string _Title;
        private ObservableCollection<TaskItem> _Tasks;

        public string Title { get { return _Title; } set { if (_Title != value) { _Title = value; OnPropertyChanged(nameof(Title)); } } }
        public ObservableCollection<TaskItem> Tasks { 
            get { return _Tasks; } 
            set { if (_Tasks != value) { _Tasks = value; _Tasks.CollectionChanged += (sender, e) => OnPropertyChanged(nameof(Tasks)); OnPropertyChanged(nameof(Tasks)); } } 
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void AddTask(TaskItem task)
        {
            Tasks.Add(task);
            task.PropertyChanged += (sender, e) => OnPropertyChanged(nameof(Tasks));
        }

        public Board(string title)
        {
            _Title = title;
            _Tasks = new ObservableCollection<TaskItem>();
            _Tasks.CollectionChanged += (sender, e) => OnPropertyChanged(nameof(Tasks));
        }
    }
}
