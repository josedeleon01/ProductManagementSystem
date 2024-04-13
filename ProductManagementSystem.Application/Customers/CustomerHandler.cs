using ProductManagementSystem.Application.Interfaces;
using ProductManagementSystem.Domain.Customers;
using System.Linq.Expressions;

namespace ProductManagementSystem.Application.Customers
{
    public class CustomerHandler(ICustomerRepository _customerRepository)
    {
        public async Task<Customer> AddAsync(Customer customer)
        {
            return await _customerRepository.AddAsync(customer);

        }
        public async Task<Customer> GetByIdAsync(int customerId)
        {
            return await _customerRepository.GetByIdAsync(customerId);

        }
        public async Task<IEnumerable<Customer>> GetAllAsync(Expression<Func<Customer, bool>> predicate = null)
        {
            return await _customerRepository.GetAllAsync(predicate);
            
        }
        public async Task RemoveAsync(Customer customer)
        {
            await _customerRepository.RemoveAsync(customer);

        }
        public async Task<Customer> UpdateAsync(Customer customer)
        {
            return await _customerRepository.UpdateAsync(customer);

        }
    }
}
