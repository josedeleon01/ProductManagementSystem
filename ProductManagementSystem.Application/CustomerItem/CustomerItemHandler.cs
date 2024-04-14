using ProductManagementSystem.Application.Interfaces;
using System.Linq.Expressions;

namespace ProductManagementSystem.Application.CustomerItem
{
    public class CustomerItemHandler(ICustomerItemRepository _customerItemRepository)
    {
        public async Task<Domain.CustomerItems.CustomerItem> AddAsync(Domain.CustomerItems.CustomerItem customerItem)
        {
            return await _customerItemRepository.AddAsync(customerItem);

        }
        public async Task<Domain.CustomerItems.CustomerItem> GetByIdAsync(int customerId, int itemId)
        {
            return await _customerItemRepository.GetByIdAsync(customerId, itemId);

        }
        public async Task<IEnumerable<Domain.CustomerItems.CustomerItem>> GetAllAsync(Expression<Func<Domain.CustomerItems.CustomerItem, bool>> predicate = null)
        {
            return await _customerItemRepository.GetAllAsync(predicate);

        }
        public async Task RemoveAsync(Domain.CustomerItems.CustomerItem customerItem)
        {
            await _customerItemRepository.RemoveAsync(customerItem);

        }
        public async Task<Domain.CustomerItems.CustomerItem> UpdateAsync(Domain.CustomerItems.CustomerItem customerItem)
        {
            return await _customerItemRepository.UpdateAsync(customerItem);

        }
        public async Task<IEnumerable<Domain.CustomerItems.CustomerItem>> GetTopExpensiveItemsAsync(int customerId, int top)
        {
            // Retrieve customer items for the specified customer ID
            var customerItems = await GetAllAsync(ci => ci.CustomerId == customerId);
            return customerItems.OrderByDescending(c => c.Item.Price).Take(top);
        }
    }
}
