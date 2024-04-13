using ProductManagementSystem.Domain.Categories;
using ProductManagementSystem.Shared.Dtos.Categories;
using System.Linq.Expressions;
namespace ProductManagementSystem.Client.Interfaces.Categories
{
    public interface ICategoryService
    {
        Task<Category> AddAsync(CategoryCreateRequest category);
        Task<Category> GetByIdAsync(int categoryId);
		Task<IEnumerable<Category>> GetAllAsync();
		Task RemoveAsync(int categoryId);
        Task<Category> UpdateAsync(CategoryUpdateRequest category);
    }
}
