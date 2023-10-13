using System.ComponentModel;
using System.Text.Json.Serialization;

namespace Common.Models
{
    public class BlazorBoardData : INotifyPropertyChanged
    {
        private ObservableList<Board> _Boards;
        private ObservableList<Label> _Labels;

        public ObservableList<Board> Boards
        {
            get { return _Boards; }
            set { if (_Boards != value) { _Boards = value; _Boards.PropertyChanged += (sender, e) => OnPropertyChanged(nameof(Boards)); OnPropertyChanged(nameof(Boards)); } }
        }
        public ObservableList<Label> Labels
        {
            get { return _Labels; }
            set { if (_Labels != value) { _Labels = value; _Labels.PropertyChanged += (sender, e) => OnPropertyChanged(nameof(Labels)); OnPropertyChanged(nameof(Labels)); } }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public BlazorBoardData()
        {
            _Boards = new();
            _Boards.PropertyChanged += (sender, e) => OnPropertyChanged(nameof(Boards));
            _Labels = new();
            _Labels.PropertyChanged += (sender, e) => OnPropertyChanged(nameof(Labels));
        }

        [JsonConstructor]
        public BlazorBoardData(ObservableList<Board> Boards, ObservableList<Label> Labels)
        {
            _Boards = Boards;
            _Boards.PropertyChanged += (sender, e) => OnPropertyChanged(nameof(Boards));
            _Labels = Labels;
            _Labels.PropertyChanged += (sender, e) => OnPropertyChanged(nameof(Labels));
        }
    }
}
