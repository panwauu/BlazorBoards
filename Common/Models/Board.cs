﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Common.Models
{
    public class Board : INotifyPropertyChanged
    {
        [Required]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        private string _Title;
        private ObservableList<TaskItem> _Tasks;

        [Required]
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

        [JsonConstructor]
        public Board(string Id, string Title, ObservableList<TaskItem> Tasks)
        {
            this.Id = Id;
            _Title = Title;
            _Tasks = Tasks;
            _Tasks.PropertyChanged += (sender, e) => OnPropertyChanged(nameof(Tasks));
        }
    }
}
