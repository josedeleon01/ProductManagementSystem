using ProductManagementSystem.Shared.Dtos.CustomerItem;

namespace ProductManagementSystem.Client.Interfaces.CustomerItems
{
    public interface ICustomerItemService
    {
        Task<Domain.CustomerItems.CustomerItem> AddAsync(CustomerItemCreateRequest CustomerItem);
        Task<Domain.CustomerItems.CustomerItem> GetByIdAsync(int customerId, int ItemId);
        Task<IEnumerable<Domain.CustomerItems.CustomerItem>> GetAllAsync();
        Task RemoveAsync(int customerId, int ItemId);
        Task<Domain.CustomerItems.CustomerItem> UpdateAsync(CustomerItemUpdateRequest CustomerItem);
        Task<IEnumerable<Domain.CustomerItems.CustomerItem>>GetAllByItemRangeAsync(int itemNumberFrom, int itemNumberTo);
        Task<List<List<Domain.CustomerItems.CustomerItem>>> GetTopExpensiveItems(int top);
    }
}
