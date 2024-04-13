using ProductManagementSystem.Client.Interfaces.Customers;
using ProductManagementSystem.Domain.Customers;
using ProductManagementSystem.Shared.Dtos.Customers;
using System.Net.Http.Json;

namespace ProductManagementSystem.Client.Services.Customers
{
    public class CustomerService(IHttpClientFactory httpClient) : ICustomerService
    {
        private readonly IHttpClientFactory _httpClient = httpClient;

        public async Task<Customer> AddAsync(CustomerCreateRequest customer)
        {
            var response = await _httpClient.CreateClient("ServerApi").PostAsJsonAsync("api/customers", customer);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Customer>();
        }

        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
            var response = await _httpClient.CreateClient("ServerApi").GetFromJsonAsync<IEnumerable<Customer>>($"api/customers");
            return response;
        }

        public async Task<Customer> GetByIdAsync(int customerId)
        {
            var response = await _httpClient.CreateClient("ServerApi").GetFromJsonAsync<Customer>($"api/customers/{customerId}");
            return response;
        }

        public async Task RemoveAsync(int customerId)
        {
            var response = await _httpClient.CreateClient("ServerApi").DeleteAsync($"api/customers/{customerId}");
            response.EnsureSuccessStatusCode();
        }

        public async Task<Customer> UpdateAsync(CustomerUpdateRequest customer)
        {
            var response = await _httpClient.CreateClient("ServerApi").PutAsJsonAsync($"api/customers/{customer.Id}", customer);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Customer>();
        }
    }
}
