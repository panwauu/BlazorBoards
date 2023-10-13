using System.ComponentModel;

namespace Common.Models
{
    public class Label : INotifyPropertyChanged
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        private string _Title;
        private string _Color;
        private string _Background;

        public string Title { get { return _Title; } set { if (_Title != value) { _Title = value; OnPropertyChanged(nameof(Title)); } } }
        public string Color { get { return _Color; } set { if (_Color != value) { _Color = value; OnPropertyChanged(nameof(Color)); } } }
        public string Background { get { return _Background; } set { if (_Background != value) { _Background = value; OnPropertyChanged(nameof(Background)); } } }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        public Label(string title, string color, string background)
        {
            _Title = title;
            _Color = color;
            _Background = background;
        }

        public Label(string Id, string title, string color, string background)
        {
            this.Id = Id;
            _Title = title;
            _Color = color;
            _Background = background;
        }
    }
}