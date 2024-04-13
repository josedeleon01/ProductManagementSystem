using Microsoft.EntityFrameworkCore;
using ProductManagementSystem.Application.Interfaces;
using ProductManagementSystem.Domain.Item;
using System.Linq.Expressions;

namespace ProductManagementSystem.Infrastructure.Persistance.Items
{
    public class ItemRepository(AppDbContext _dbContext) : IItemRepository
    {
        public async Task<Item> AddAsync(Item item)
        {
            item.Category = null;
            await _dbContext.AddAsync(item);
            await _dbContext.SaveChangesAsync();

            return item;
        }

        public async Task<Item> GetByIdAsync(int itemId)
        {
            return await _dbContext.Items
                .Include(item => item.Category)
                .FirstOrDefaultAsync(x => x.Id == itemId);
        }

        public async Task<IEnumerable<Item>> GetAllAsync(Expression<Func<Item, bool>> predicate = null)
        {
            IQueryable<Item> query = _dbContext.Items;

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            query = query.Include(item => item.Category);

            return await query.ToListAsync();
        }
        public async Task RemoveAsync(Item item)
        {
            _dbContext.Remove(item);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Item> UpdateAsync(Item item)
        {
            item.Category = null;
            _dbContext.Update(item);
            await _dbContext.SaveChangesAsync();

            return item;
        }
    }
}
