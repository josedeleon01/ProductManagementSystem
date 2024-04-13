using Microsoft.EntityFrameworkCore;
using ProductManagementSystem.Application.Interfaces;
using ProductManagementSystem.Domain.Customers;
using System.Linq.Expressions;

namespace ProductManagementSystem.Infrastructure.Persistance.Customers
{
    public class CustomerRepository(AppDbContext _dbContext) : ICustomerRepository
    {
        public async Task<Customer> AddAsync(Customer customer)
        {
            await _dbContext.AddAsync(customer);
            await _dbContext.SaveChangesAsync();

            return customer;
        }

        public async Task<Customer> GetByIdAsync(int customerId)
        {
            return await _dbContext.Customers.FindAsync(customerId);
        }

        public async Task<IEnumerable<Customer>> GetAllAsync(Expression<Func<Customer, bool>> predicate = null)
        {
            IQueryable<Customer> query = _dbContext.Customers;

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            return await query.ToListAsync();
        }
        public async Task RemoveAsync(Customer customer)
        {
            _dbContext.Remove(customer);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Customer> UpdateAsync(Customer customer)
        {
            _dbContext.Update(customer);
            await _dbContext.SaveChangesAsync();

            return customer;
        }
    }
}
