using System.Linq.Expressions;

namespace ProductManagementSystem.Application.Interfaces
{
    public interface ICustomerItemRepository
    {
        Task<Domain.CustomerItems.CustomerItem> AddAsync(Domain.CustomerItems.CustomerItem customerItem);
        Task<Domain.CustomerItems.CustomerItem> GetByIdAsync(int customerId, int ItemId);
        Task<IEnumerable<Domain.CustomerItems.CustomerItem>> GetAllAsync(Expression<Func<Domain.CustomerItems.CustomerItem, bool>> predicate = null);
        Task RemoveAsync(Domain.CustomerItems.CustomerItem customerItem);
        Task<Domain.CustomerItems.CustomerItem> UpdateAsync(Domain.CustomerItems.CustomerItem customerItem);

    }
}
