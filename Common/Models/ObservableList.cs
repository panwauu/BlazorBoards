using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Common.Models
{
    public class ObservableList<T> : INotifyPropertyChanged, IList<T> where T : INotifyPropertyChanged
    {
        private readonly ObservableCollection<T> _Items;

        public T this[int index]
        {
            get => _Items[index];
            set { _Items[index] = value; value.PropertyChanged += (sender, e) => OnPropertyChanged(nameof(Count)); }
        }

        public void Add(T item)
        {
            _Items.Add(item);
            item.PropertyChanged += (sender, e) => OnPropertyChanged(nameof(Count));
        }

        public void Insert(int index, T item)
        {
            _Items.Insert(index, item);
            item.PropertyChanged += (sender, e) => OnPropertyChanged(nameof(Count));
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ObservableList()
        {
            _Items = new ObservableCollection<T>();
            _Items.CollectionChanged += (sender, e) => OnPropertyChanged(nameof(Count));
        }

        public void CopyTo(T[] array, int arrayIndex) => _Items.CopyTo(array, arrayIndex);

        public int Count => _Items.Count;

        public bool IsReadOnly => false;

        public void Clear() => _Items.Clear();

        public bool Contains(T item) => _Items.Contains(item);

        public IEnumerator<T> GetEnumerator() => _Items.GetEnumerator();

        public int IndexOf(T item) => _Items.IndexOf(item);

        public bool Remove(T item) => _Items.Remove(item);

        public void RemoveAt(int index) => _Items.RemoveAt(index);

        IEnumerator IEnumerable.GetEnumerator() => _Items.GetEnumerator();
    }
}
