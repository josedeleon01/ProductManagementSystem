using ProductManagementSystem.Client.Interfaces.Categories;
using ProductManagementSystem.Domain.Categories;
using ProductManagementSystem.Shared.Dtos.Categories;
using System.Net.Http.Json;

namespace ProductManagementSystem.Client.Services.Categories
{
    public class CategoryService(IHttpClientFactory httpClient) : ICategoryService
    {
        private readonly IHttpClientFactory _httpClient = httpClient;

        public async Task<Category> AddAsync(CategoryCreateRequest category)
        {
            var response = await _httpClient.CreateClient("ServerApi").PostAsJsonAsync("api/categories", category);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Category>();
        }

		public async Task<IEnumerable<Category>> GetAllAsync()
		{
            var response = await _httpClient.CreateClient("ServerApi").GetFromJsonAsync<IEnumerable<Category>>($"api/categories");
            return response;    
		}

		public async Task<Category> GetByIdAsync(int categoryId)
        {
            var response = await _httpClient.CreateClient("ServerApi").GetFromJsonAsync<Category>($"api/categories/{categoryId}");
            return response;
        }

        public async Task RemoveAsync(int categoryId)
        {
            var response = await _httpClient.CreateClient("ServerApi").DeleteAsync($"api/categories/{categoryId}");
            response.EnsureSuccessStatusCode();
        }

        public async Task<Category> UpdateAsync(CategoryUpdateRequest category)
        {
            var response = await _httpClient.CreateClient("ServerApi").PutAsJsonAsync($"api/categories/{category.Id}", category);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Category>();
        }
    }
}
