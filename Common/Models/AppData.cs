using System.Collections.ObjectModel;
using Blazored.LocalStorage;
using System.Net.Http.Json;

namespace Common.Models
{
    public class AppData
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorage;

        public ObservableCollection<Label> LabelItems { get; set; }

        private async void PushToServer()
        {
            await _localStorage.SetItemAsync("LabelItems", LabelItems);
            try
            {
                await _httpClient.PutAsJsonAsync("api/labels", LabelItems);
                await _localStorage.RemoveItemAsync("OfflineChanges");
            }
            catch (Exception ex)
            {
                await _localStorage.SetItemAsync("OfflineChanges", DateTime.Now);
                Console.WriteLine(ex.Message);
            }
        }

        public void ChangedHandler(object? sender, EventArgs args)
        {
            PushToServer();
        }

        public async Task Initialize()
        {
            try
            {
                var labelsFromServer = await _httpClient.GetFromJsonAsync<List<Label>>("api/labels");
                await _localStorage.RemoveItemAsync("OfflineChanges");
                if (labelsFromServer is null) return;
                foreach (var label in labelsFromServer)
                {
                    LabelItems.Add(label);
                    label.PropertyChanged += ChangedHandler;
                }
            }
            catch (Exception ex)
            {
                await _localStorage.SetItemAsync("OfflineChanges", DateTime.Now);
                var labelsFromStorage = await _localStorage.GetItemAsync<ObservableCollection<Label>>("LabelItems");
                if (labelsFromStorage is null) return;
                foreach (var label in labelsFromStorage)
                {
                    LabelItems.Add(label);
                    label.PropertyChanged += ChangedHandler;
                }
            }
        }

        public AppData(HttpClient httpClient, ILocalStorageService localStorage)
        {
            _httpClient = httpClient;
            _localStorage = localStorage;
            LabelItems = new ObservableCollection<Label>();
            LabelItems.CollectionChanged += ChangedHandler;
        }
    }
}
