using ProductManagementSystem.Domain.Customers;
using ProductManagementSystem.Shared.Dtos.Customers;
using System.Linq.Expressions;

namespace ProductManagementSystem.Client.Interfaces.Customers
{
    public interface ICustomerService
    {
        Task<Customer> AddAsync(CustomerCreateRequest customer);
        Task<Customer> GetByIdAsync(int customerId);
        Task<IEnumerable<Customer>> GetAllAsync();
        Task RemoveAsync(int customerId);
        Task<Customer> UpdateAsync(CustomerUpdateRequest customer);
    }
}
