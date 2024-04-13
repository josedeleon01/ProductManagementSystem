using ProductManagementSystem.Client.Interfaces.CustomerItems;
using ProductManagementSystem.Domain.CustomerItems;
using ProductManagementSystem.Shared.Dtos.CustomerItem;
using System.Net;
using System.Net.Http.Json;

namespace ProductManagementSystem.Client.Services.CustomerItems
{
    public class CustomerItemService(IHttpClientFactory httpClient) : ICustomerItemService
    {
        private readonly IHttpClientFactory _httpClient = httpClient;

        public async Task<CustomerItem> AddAsync(CustomerItemCreateRequest customerItem)
        {
            var response = await _httpClient.CreateClient("ServerApi").PostAsJsonAsync("api/customerItem", customerItem);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<CustomerItem>();
        }

        public async Task<IEnumerable<CustomerItem>> GetAllAsync()
        {
            var response = await _httpClient.CreateClient("ServerApi").GetFromJsonAsync<IEnumerable<CustomerItem>>($"api/customerItem");
            return response;
        }

        public async Task<CustomerItem> GetByIdAsync(int customerId, int ItemId)
        {
            var response = await _httpClient.CreateClient("ServerApi").GetAsync($"api/customerItem/{customerId}/{ItemId}");
            if (response.StatusCode == HttpStatusCode.NotFound)
                return null;

            return await response.Content.ReadFromJsonAsync<CustomerItem>();
        }

        public async Task RemoveAsync(int customerId, int itemId)
        {
            var response = await _httpClient.CreateClient("ServerApi").DeleteAsync($"api/customerItem/{customerId}/{itemId}");
            response.EnsureSuccessStatusCode();
        }
        public async Task<CustomerItem> UpdateAsync(CustomerItemUpdateRequest customerItem)
        {
            var response = await _httpClient.CreateClient("ServerApi").PutAsJsonAsync($"api/customerItem/{customerItem.CustomerId}/{customerItem.ItemId}", customerItem);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<CustomerItem>();
        }
        public async Task<List<List<CustomerItem>>> GetTopExpensiveItems(int top)
        {
            var response = await _httpClient.CreateClient("ServerApi").GetAsync($"api/customerItem/getTopExpensiveItems/{top}");
            if (response.StatusCode == HttpStatusCode.NotFound)
                return null;

            return await response.Content.ReadFromJsonAsync<List<List<CustomerItem>>>();
        }
        public async Task<IEnumerable<CustomerItem>>GetAllByItemRangeAsync(int itemNumberFrom, int itemNumberTo)
        {
            var response = await _httpClient.CreateClient("ServerApi").GetAsync($"api/customerItem/getByItemRange/{itemNumberFrom}/{itemNumberTo}");

            if (response.StatusCode == HttpStatusCode.NotFound)
                return null;

            return await response.Content.ReadFromJsonAsync<IEnumerable<CustomerItem>>();

        }
    }
}
