using ProductManagementSystem.Domain.Item;
using ProductManagementSystem.Shared.Dtos.Items;
using System.Linq.Expressions;

namespace ProductManagementSystem.Client.Interfaces.Items
{
    public interface IItemService
    {
        Task<Item> AddAsync(ItemCreateRequest item);
        Task<Item> GetByIdAsync(int itemId);
        Task<IEnumerable<Item>> GetAllAsync();
        Task RemoveAsync(int itemId);
        Task<Item> UpdateAsync(ItemUpdateRequest item);
    }
}
