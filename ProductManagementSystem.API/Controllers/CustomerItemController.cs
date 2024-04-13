using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductManagementSystem.Application.CustomerItem;
using ProductManagementSystem.Application.Customers;
using ProductManagementSystem.Domain.CustomerItems;
using ProductManagementSystem.Shared.Dtos.CustomerItem;

namespace ProductManagementSystem.API.Controllers
{
    /// <summary>
    /// Controller for managing customer items.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CustomerItemController(CustomerItemHandler customerItemHandler, CustomerHandler customerHandler, IMapper _mapper) : ControllerBase
    {
        /// <summary>
        /// Retrieves all customer items.
        /// </summary>
        /// <returns>An action result containing the list of customer items.</returns>
        // GET: api/<CustomerItemController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var customerItem = await customerItemHandler.GetAllAsync();
            return Ok(customerItem);
        }

        /// <summary>
        /// Retrieves a customer item by its customer ID and item ID.
        /// </summary>
        /// <param name="customerId">The ID of the customer.</param>
        /// <param name="itemId">The ID of the item.</param>
        /// <returns>An action result containing the retrieved customer item, or NotFound if the customer item is not found.</returns>
        // GET api/<CustomerItemController>/5
        [HttpGet("{customerId}/{itemId}")]
        public async Task<IActionResult> Get(int customerId, int itemId)
        {
            var customerItem = await customerItemHandler.GetByIdAsync(customerId, itemId);
            if (customerItem == null)
                return NotFound();

            return Ok(customerItem);
        }

        /// <summary>
        /// Creates a new customer item.
        /// </summary>
        /// <param name="CustomerItemCreateRequest">The data for the new customer item.</param>
        /// <returns>An action result containing the created customer item.</returns>
        // POST api/<CustomerItemController>
        [HttpPost]
        public async Task<IActionResult> Post(CustomerItemCreateRequest CustomerItemCreateRequest)
        {
            var customerItem = _mapper.Map<CustomerItem>(CustomerItemCreateRequest);
            var addedCustomerItem = await customerItemHandler.AddAsync(customerItem);
            return CreatedAtAction(nameof(Get), new { customerId = CustomerItemCreateRequest.CustomerId, itemId = CustomerItemCreateRequest.ItemId }, addedCustomerItem);
        }

        /// <summary>
        /// Updates a customer item by its customer ID and item ID.
        /// </summary>
        /// <param name="customerId">The ID of the customer.</param>
        /// <param name="itemId">The ID of the item.</param>
        /// <param name="CustomerItemUpdateRequest">The updated data for the customer item.</param>
        /// <returns>An action result containing the updated customer item, or NotFound if the customer item is not found, or BadRequest if the provided IDs do not match the IDs in the request body.</returns>
        // PUT api/<CustomerItemsController>/5
        [HttpPut("{customerId}/{itemId}")]
        public async Task<IActionResult> Put(int customerId, int itemId, CustomerItemUpdateRequest CustomerItemUpdateRequest)
        {
            if (customerId != CustomerItemUpdateRequest.CustomerId || itemId != CustomerItemUpdateRequest.ItemId)
                return BadRequest("The provided Id does not match the Id in the request body.");

            var customerItem = _mapper.Map<CustomerItem>(CustomerItemUpdateRequest);

            var updatedCustomerItem = await customerItemHandler.UpdateAsync(customerItem);
            if (updatedCustomerItem == null)
                return NotFound();

            return Ok(updatedCustomerItem);
        }

        /// <summary>
        /// Deletes a customer item by its customer ID and item ID.
        /// </summary>
        /// <param name="customerId">The ID of the customer.</param>
        /// <param name="itemId">The ID of the item.</param>
        /// <returns>An action result indicating success, or NotFound if the customer item is not found.</returns>
        // DELETE api/<CustomerItemsController>/5
        [HttpDelete("{customerId}/{itemId}")]
        public async Task<IActionResult> Delete(int customerId, int itemId)
        {
            var customerItem = await customerItemHandler.GetByIdAsync(customerId, itemId);
            if (customerItem == null)
                return NotFound();

            await customerItemHandler.RemoveAsync(customerItem);
            return NoContent();
        }

        /// <summary>
        /// Retrieves a list of Customer items by its item range number specified.
        /// </summary>
        /// <param name="itemNumberFrom"></param>
        /// <param name="itemNumberTo"></param>
        /// <returns>An action result containing the retrieved item list, or NotFound if the items are not found.</returns>
        // GET api/<ItemsController>/5
        [HttpGet("getByItemRange/{itemNumberFrom}/{itemNumberTo}")]
        public async Task<IActionResult> GetByItemRange(int itemNumberFrom, int itemNumberTo)
        {
            var customerItems = await customerItemHandler.GetAllAsync(x => x.Item.ItemNumber >= itemNumberFrom && x.Item.ItemNumber <= itemNumberTo);
            if (customerItems== null || !customerItems.Any())
                return NotFound();

            return Ok(customerItems);
        }

        /// <summary>
        /// Retrieves a list to get the top most expensive Items.
        /// </summary>
        /// <param name="top"></param>
        /// <returns>An action result containing the retrieved customer item list, or NotFound if customers  are not found.</returns>
        // GET api/<ItemsController>/5
        [HttpGet("getTopExpensiveItems/{top}")]
        public async Task<IActionResult> Get(int top)
        {
            var customers = await customerHandler.GetAllAsync();
            List<List<CustomerItem>> result = [];

            if (customers == null || !customers.Any())
                return NotFound();

            foreach (var customer in customers)
            {
                var customerItems = await customerItemHandler.GetAllAsync(x => x.CustomerId == customer.Id);

                if (customerItems != null && customerItems.Any())
                {
                    var topItems = customerItems.OrderByDescending(c => c.Price)
                                                .Take(top)
                                                .ToList();
                    result.Add(topItems);
                }
            }

            return Ok(result);
        }
    }
}
