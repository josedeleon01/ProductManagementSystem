using ProductManagementSystem.Application.Interfaces;
using ProductManagementSystem.Domain.Item;
using System.Linq.Expressions;

namespace ProductManagementSystem.Application.Items
{
    public class ItemHandler(IItemRepository _itemRepository)
    {
        public async Task<Item> AddAsync(Item item)
        {
            return await _itemRepository.AddAsync(item);

        }
        public async Task<Item> GetByIdAsync(int itemId)
        {
            return await _itemRepository.GetByIdAsync(itemId);

        }
        public async Task<IEnumerable<Item>> GetAllAsync(Expression<Func<Item, bool>> predicate = null)
        {
            return await _itemRepository.GetAllAsync(predicate);

        }
        public async Task RemoveAsync(Item item)
        {
            await _itemRepository.RemoveAsync(item);

        }
        public async Task<Item> UpdateAsync(Item item)
        {
            return await _itemRepository.UpdateAsync(item);

        }

    }
}
