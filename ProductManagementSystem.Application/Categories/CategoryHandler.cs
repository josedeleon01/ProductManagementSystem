using ProductManagementSystem.Application.Interfaces;
using ProductManagementSystem.Domain.Categories;
using System.Linq.Expressions;

namespace ProductManagementSystem.Application.Categories
{
    public class CategoryHandler(ICategoryRepository _categoryRepository)
    {
        public async Task<Category> AddAsync(Category category)
        {
            return await _categoryRepository.AddAsync(category);
        
        }
        public async Task<Category> GetByIdAsync(int categoryId)
        {
            return await _categoryRepository.GetByIdAsync(categoryId);

        }
        public async Task<IEnumerable<Category>> GetAllAsync(Expression<Func<Category, bool>> predicate = null)
        {
            return await _categoryRepository.GetAllAsync(predicate);
        
        }
        public async Task RemoveAsync(Category category)
        {
            await _categoryRepository.RemoveAsync(category);

        }
        public async Task<Category> UpdateAsync(Category category)
        {
            return await _categoryRepository.UpdateAsync(category);
        
        }

    }
}
