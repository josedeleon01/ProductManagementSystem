using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProductManagementSystem.Application.Customers;
using ProductManagementSystem.Shared.Dtos.Customers;
using ProductManagementSystem.Domain.Customers;
using Microsoft.AspNetCore.Authorization;

namespace ProductManagementSystem.API.Controllers
{
    /// <summary>
    /// Controller for managing customers.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CustomersController(CustomerHandler customerHandler, IMapper _mapper) : ControllerBase
    {
        /// <summary>
        /// Retrieves all customers.
        /// </summary>
        /// <returns>An action result containing the list of customers.</returns>
        // GET: api/<CustomersController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var customers = await customerHandler.GetAllAsync();
            return Ok(customers);
        }

        /// <summary>
        /// Retrieves a customer by its ID.
        /// </summary>
        /// <param name="id">The ID of the customer to retrieve.</param>
        /// <returns>An action result containing the retrieved customer, or NotFound if the customer is not found.</returns>
        // GET api/<CustomersController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var customer = await customerHandler.GetByIdAsync(id);
            if (customer == null)
                return NotFound();

            return Ok(customer);
        }

        /// <summary>
        /// Creates a new customer.
        /// </summary>
        /// <param name="customerRequest">The data for the new customer.</param>
        /// <returns>An action result containing the created customer.</returns>
        // POST api/<CustomersController>
        [HttpPost]
        public async Task<IActionResult> Post(CustomerCreateRequest customerRequest)
        {
            var customer = _mapper.Map<Customer>(customerRequest);
            var addedCustomer = await customerHandler.AddAsync(customer);
            return CreatedAtAction(nameof(Get), new { id = addedCustomer.Id }, addedCustomer);
        }

        /// <summary>
        /// Updates a customer by its ID.
        /// </summary>
        /// <param name="id">The ID of the customer to update.</param>
        /// <param name="customerRequest">The updated data for the customer.</param>
        /// <returns>An action result containing the updated customer, or NotFound if the customer is not found, or BadRequest if the provided ID does not match the ID in the request body.</returns>
        // PUT api/<CustomersController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, CustomerUpdateRequest customerRequest)
        {
            if (id != customerRequest.Id)
                return BadRequest("The provided Id does not match the Id in the request body.");

            var customer = _mapper.Map<Customer>(customerRequest);

            var updatedCustomer = await customerHandler.UpdateAsync(customer);
            if (updatedCustomer == null)
                return NotFound();

            return Ok(updatedCustomer);
        }

        /// <summary>
        /// Deletes a customer by its ID.
        /// </summary>
        /// <param name="id">The ID of the customer to delete.</param>
        /// <returns>An action result indicating success, or NotFound if the customer is not found.</returns>
        // DELETE api/<CustomersController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var customer = await customerHandler.GetByIdAsync(id);
            if (customer == null)
                return NotFound();

            await customerHandler.RemoveAsync(customer);
            return NoContent();
        }
    }
}
