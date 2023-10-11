using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Common.Models
{
    public class BlazorBoardData : INotifyPropertyChanged
    {
        private ObservableCollection<Board> _Boards;
        private ObservableCollection<Label> _Labels;

        public ObservableCollection<Board> Boards
        {
            get { return _Boards; }
            set { if (_Boards != value) { _Boards = value; _Boards.CollectionChanged += (sender, e) => OnPropertyChanged(nameof(Boards)); OnPropertyChanged(nameof(Boards)); } }
        }
        public ObservableCollection<Label> Labels
        {
            get { return _Labels; }
            set { if (_Labels != value) { _Labels = value; _Labels.CollectionChanged += (sender, e) => OnPropertyChanged(nameof(Labels)); OnPropertyChanged(nameof(Labels)); } }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void OverrideHandlers(BlazorBoardData dataToOverride)
        {
            var newData = new BlazorBoardData();
            foreach (var board in dataToOverride.Boards)
            {
                var newBoard = new Board(board.Title);
                foreach (var task in board.Tasks)
                {
                    var newTask = new TaskItem(task.Title);
                    foreach (var checklist in task.Checklist) { newTask.Checklist.Add(new ChecklistItem(checklist.Title)); }
                    foreach (var label in task.Labels) { newTask.Labels.Add(label); }
                    newTask.Description = task.Description;
                    newTask.Deadline = task.Deadline;
                    newBoard.AddTask(newTask);
                }
                newData.Boards.Add(newBoard);
            }
            foreach (var label in dataToOverride.Labels)
            {
                newData.Labels.Add(new Label(label.Title, label.Color, label.Background));
            }

            _Boards = newData.Boards;
            _Boards.CollectionChanged += (sender, e) => OnPropertyChanged(nameof(Boards));
            _Labels = newData.Labels;
            _Labels.CollectionChanged += (sender, e) => OnPropertyChanged(nameof(Labels));
        }

        public BlazorBoardData()
        {
            _Boards = new();
            _Boards.CollectionChanged += (sender, e) => OnPropertyChanged(nameof(Boards));
            _Labels = new();
            _Labels.CollectionChanged += (sender, e) => OnPropertyChanged(nameof(Labels));
        }
    }
}
