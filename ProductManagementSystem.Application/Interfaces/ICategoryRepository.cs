using ProductManagementSystem.Domain.Categories;
using System.Linq.Expressions;

namespace ProductManagementSystem.Application.Interfaces
{
    public interface ICategoryRepository
    {
        Task<Category> AddAsync(Category category);
        Task<Category> GetByIdAsync(int categoryId);
        Task<IEnumerable<Category>> GetAllAsync(Expression<Func<Category, bool>> predicate = null);
        Task RemoveAsync(Category category);
        Task<Category> UpdateAsync(Category category);

    }
}
