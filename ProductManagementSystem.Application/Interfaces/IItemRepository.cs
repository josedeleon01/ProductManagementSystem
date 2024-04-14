using ProductManagementSystem.Domain.Item;
using System.Linq.Expressions;

namespace ProductManagementSystem.Application.Interfaces
{
    public interface IItemRepository
    {
        Task<Item> AddAsync(Item item);
        Task<Item> GetByIdAsync(int itemId);
        Task<IEnumerable<Item>> GetAllAsync(Expression<Func<Item, bool>> predicate = null);
        Task RemoveAsync(Item item);
        Task<Item> UpdateAsync(Item item);
    }
}
