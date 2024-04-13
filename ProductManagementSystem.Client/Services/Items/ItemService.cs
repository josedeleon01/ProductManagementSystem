using ProductManagementSystem.Client.Interfaces.Items;
using ProductManagementSystem.Domain.Item;
using ProductManagementSystem.Shared.Dtos.Items;
using System.Net.Http.Json;

namespace ProductManagementSystem.Client.Services.Items
{
    public class ItemService(IHttpClientFactory httpClient) : IItemService
    {
        private readonly IHttpClientFactory _httpClient = httpClient;

        public async Task<Item> AddAsync(ItemCreateRequest item)
        {
            var response = await _httpClient.CreateClient("ServerApi").PostAsJsonAsync("api/items", item);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Item>();
        }

        public async Task<IEnumerable<Item>> GetAllAsync()
        {
            var response = await _httpClient.CreateClient("ServerApi").GetFromJsonAsync<IEnumerable<Item>>($"api/items");
            return response;

        }

        public async Task<Item> GetByIdAsync(int itemId)
        {
            var response = await _httpClient.CreateClient("ServerApi").GetFromJsonAsync<Item>($"api/items/{itemId}");
            return response;
        }

        public async Task RemoveAsync(int itemId)
        {
            var response = await _httpClient.CreateClient("ServerApi").DeleteAsync($"api/items/{itemId}");
            response.EnsureSuccessStatusCode();
        }

        public async Task<Item> UpdateAsync(ItemUpdateRequest item)
        {
            var response = await _httpClient.CreateClient("ServerApi").PutAsJsonAsync($"api/items/{item.Id}", item);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Item>();
        }
    }
}
