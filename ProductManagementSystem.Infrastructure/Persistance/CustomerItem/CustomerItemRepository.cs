using Microsoft.EntityFrameworkCore;
using ProductManagementSystem.Application.Interfaces;
using System.Linq.Expressions;

namespace ProductManagementSystem.Infrastructure.Persistance.CustomerItem
{
    public class CustomerItemRepository(AppDbContext _dbContext) : ICustomerItemRepository
    {
        public async Task<Domain.CustomerItems.CustomerItem> AddAsync(Domain.CustomerItems.CustomerItem customerItems)
        {
            customerItems.Item = null;
            customerItems.Customer = null;
            await _dbContext.AddAsync(customerItems);
            await _dbContext.SaveChangesAsync();

            return customerItems;
        }

        public async Task<Domain.CustomerItems.CustomerItem> GetByIdAsync(int customerId, int ItemId)
        {
            return await _dbContext.CustomerItems
                .Include(c => c.Customer)
                .Include(x => x.Item)
                .FirstOrDefaultAsync(x => x.CustomerId == customerId && x.ItemId == ItemId);
        }

        public async Task<IEnumerable<Domain.CustomerItems.CustomerItem>> GetAllAsync(Expression<Func<Domain.CustomerItems.CustomerItem, bool>> predicate = null)
        {
            IQueryable<Domain.CustomerItems.CustomerItem> query = _dbContext.CustomerItems;

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            query = query.Include(x => x.Item)
                .Include(c => c.Customer);

            return await query.ToListAsync();
        }
        public async Task RemoveAsync(Domain.CustomerItems.CustomerItem customerItems)
        {
            _dbContext.Remove(customerItems);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Domain.CustomerItems.CustomerItem> UpdateAsync(Domain.CustomerItems.CustomerItem customerItems)
        {
            customerItems.Item = null;
            customerItems.Customer = null;
            _dbContext.Update(customerItems);
            await _dbContext.SaveChangesAsync();

            return customerItems;
        }
    }
}
