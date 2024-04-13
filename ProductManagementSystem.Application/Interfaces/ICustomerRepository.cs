using ProductManagementSystem.Domain.Customers;
using System.Linq.Expressions;

namespace ProductManagementSystem.Application.Interfaces
{
    public interface ICustomerRepository
    {
        Task<Customer> AddAsync(Customer customer);
        Task<Customer> GetByIdAsync(int customerId);
        Task<IEnumerable<Customer>> GetAllAsync(Expression<Func<Customer, bool>> predicate = null);
        Task RemoveAsync(Customer customer);
        Task<Customer> UpdateAsync(Customer customer);
    }
}
