﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Common.Models
{
    public class Label : INotifyPropertyChanged
    {
        [Required]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        private string _Title;
        private string _Color;
        private string _Background;

        [Required]
        public string Title { get { return _Title; } set { if (_Title != value) { _Title = value; OnPropertyChanged(nameof(Title)); } } }

        [Required]
        public string Color { get { return _Color; } set { if (_Color != value) { _Color = value; OnPropertyChanged(nameof(Color)); } } }

        [Required]
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

        [JsonConstructor]
        public Label(string Id, string Title, string Color, string Background)
        {
            this.Id = Id;
            _Title = Title;
            _Color = Color;
            _Background = Background;
        }
    }
}