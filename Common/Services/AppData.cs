using Blazored.LocalStorage;
using System.Net.Http.Json;
using Common.Models;
using System.Text.Json;

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
                await _httpClient.PutAsJsonAsync("api/data", blazorBoardData);
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
                var dataFromServer = await _httpClient.GetFromJsonAsync<BlazorBoardData>("api/data");
                await _localStorage.RemoveItemAsync("OfflineChanges");
                if (dataFromServer is null) return;
                blazorBoardData.OverrideHandlers(dataFromServer);
                Console.WriteLine(JsonSerializer.Serialize(blazorBoardData));
            }
            catch (Exception ex)
            {
                await _localStorage.SetItemAsync("OfflineChanges", DateTime.Now);
                var dataFromStorage = await _localStorage.GetItemAsync<BlazorBoardData>("blazorBoardData");
                if (dataFromStorage is null) return;
                blazorBoardData.OverrideHandlers(dataFromStorage);
                Console.WriteLine(ex.Message);
                Console.WriteLine(JsonSerializer.Serialize(blazorBoardData));
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
