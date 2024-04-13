using Microsoft.EntityFrameworkCore;
using ProductManagementSystem.Application.Interfaces;
using ProductManagementSystem.Domain.Categories;
using System.Linq.Expressions;

namespace ProductManagementSystem.Infrastructure.Persistance.Categories
{
    public class CategoryRepository(AppDbContext _dbContext) : ICategoryRepository
    {

        public async Task<Category> AddAsync(Category category)
        {
            await _dbContext.AddAsync(category);
            await _dbContext.SaveChangesAsync();

            return category;
        }

        public async Task<Category> GetByIdAsync(int categoryId)
        {
            return await _dbContext.Categories.FindAsync(categoryId);
        }

        public async Task<IEnumerable<Category>> GetAllAsync(Expression<Func<Category, bool>> predicate = null)
        {
            IQueryable<Category> query = _dbContext.Categories;

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            return await query.ToListAsync();
        }
        public async Task RemoveAsync(Category category)
        {
            _dbContext.Remove(category);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Category> UpdateAsync(Category category)
        {
            _dbContext.Update(category);
            await _dbContext.SaveChangesAsync();

            return category;
        }
    }
}
