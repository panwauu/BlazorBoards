using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Common.Models
{
    public class BlazorBoardData : INotifyPropertyChanged
    {
        private ObservableCollection<Board> _Boards;
        private ObservableCollection<Label> _Labels;

        public ObservableCollection<Board> Boards { 
            get { return _Boards; } 
            set { if (_Boards != value) { _Boards = value; _Boards.CollectionChanged += (sender, e) => OnPropertyChanged(nameof(Boards)); OnPropertyChanged(nameof(Boards)); } } 
        }
        public ObservableCollection<Label> Labels { 
            get { return _Labels; } 
            set { if (_Labels != value) { _Labels = value; _Labels.CollectionChanged += (sender, e) => OnPropertyChanged(nameof(Labels)); OnPropertyChanged(nameof(Labels)); } } 
        }

        public void AddLabel(Label label)
        {
            Labels.Add(label);
            label.PropertyChanged += (sender, e) => OnPropertyChanged(nameof(Labels));
        }

        public void AddBoard(Board board)
        {
            Boards.Add(board);
            board.PropertyChanged += (sender, e) => OnPropertyChanged(nameof(Boards));
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public BlazorBoardData()
        {
            _Boards = new();
            _Boards.CollectionChanged += (sender, e) => OnPropertyChanged(nameof(Boards));
            _Labels = new();
            _Labels.CollectionChanged += (sender, e) => OnPropertyChanged(nameof(Labels));

            AddBoard(new Board("In progress") { });
            AddBoard(new Board("Backlog") { });
            AddBoard(new Board("Done") { });
            _Boards[0].AddTask(new TaskItem("Task 1") { });
            _Boards[0].AddTask(new TaskItem("Task 2") { });
            _Boards[1].AddTask(new TaskItem("Task 1") { });
            _Boards[1].AddTask(new TaskItem("Task 2") { });

            _Boards[0].Tasks[0].Labels.Add("Label 1");

            AddLabel(new Label("Label 1", "#000000", "#BBBB00") { });
        }
    }
}
