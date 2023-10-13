using Blazored.LocalStorage;
using Common.Models;
using System.Net.Http.Json;

namespace Common.Services
{
    public class AppData
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorage;

        public BlazorBoardData blazorBoardData;

        private async void PushToServer()
        {
            await _localStorage.SetItemAsync("blazorBoardData", blazorBoardData);
            try
            {
                await _httpClient.PutAsJsonAsync("api/Boards", blazorBoardData);
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
            Console.WriteLine("Initializing...");
            try
            {
                var dataFromServer = await _httpClient.GetFromJsonAsync<BlazorBoardData>("api/Boards");
                await _localStorage.RemoveItemAsync("OfflineChanges");
                if (dataFromServer is null) return;
                blazorBoardData = dataFromServer;
                blazorBoardData.PropertyChanged += ChangedHandler;
            }
            catch (Exception ex)
            {
                await _localStorage.SetItemAsync("OfflineChanges", DateTime.Now);
                var dataFromStorage = await _localStorage.GetItemAsync<BlazorBoardData>("blazorBoardData");
                if (dataFromStorage is null) return;
                blazorBoardData = dataFromStorage;
                blazorBoardData.PropertyChanged += ChangedHandler;
            }
        }

        public AppData(HttpClient httpClient, ILocalStorageService localStorage)
        {
            _httpClient = httpClient;
            _localStorage = localStorage;
            blazorBoardData = new();
            blazorBoardData.PropertyChanged += ChangedHandler;
        }
    }
}
