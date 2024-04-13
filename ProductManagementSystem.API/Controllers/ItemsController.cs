using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProductManagementSystem.Shared.Dtos.Items;
using ProductManagementSystem.Application.Items;
using ProductManagementSystem.Domain.Item;
using ProductManagementSystem.Application.Categories;
using Microsoft.AspNetCore.Authorization;

namespace ProductManagementSystem.API.Controllers
{
    /// <summary>
    /// Controller for managing items.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ItemsController(ItemHandler ItemHandler, IMapper _mapper, CategoryHandler categoryHandler) : ControllerBase
    {
        /// <summary>
        /// Retrieves all items.
        /// </summary>
        /// <returns>An action result containing the list of items.</returns>
        // GET: api/<ItemsController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var items = await ItemHandler.GetAllAsync();
            return Ok(items);
        }

        /// <summary>
        /// Retrieves an item by its ID.
        /// </summary>
        /// <param name="id">The ID of the item to retrieve.</param>
        /// <returns>An action result containing the retrieved item, or NotFound if the item is not found.</returns>
        // GET api/<ItemsController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var item = await ItemHandler.GetByIdAsync(id);
            if (item == null)
                return NotFound();

            return Ok(item);
        }

        /// <summary>
        /// Creates a new item.
        /// </summary>
        /// <param name="itemRequest">The data for the new item.</param>
        /// <returns>An action result containing the created item, or NotFound if the category with the provided CategoryId is not found.</returns>
        // POST api/<ItemsController>
        [HttpPost]
        public async Task<IActionResult> Post(ItemCreateRequest itemRequest)
        {
            var category = await categoryHandler.GetByIdAsync(itemRequest.CategoryId);
            if (category == null)
            {
                // If category is null, return a 404 Not Found response.
                return NotFound("Category not found with the provided CategoryId.");
            }

            var item = _mapper.Map<Item>(itemRequest);
            var addedItem = await ItemHandler.AddAsync(item);
            return CreatedAtAction(nameof(Get), new { id = addedItem.Id }, addedItem);
        }

        /// <summary>
        /// Updates an item by its ID.
        /// </summary>
        /// <param name="id">The ID of the item to update.</param>
        /// <param name="itemRequest"></param>
        /// <returns>An action result containing the updated item, or NotFound if the item is not found, or BadRequest if the provided ID does not match the ID in the request body.</returns>
        // PUT api/<ItemsController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, ItemUpdateRequest itemRequest)
        {
            if (id != itemRequest.Id)
                return BadRequest("The provided Id does not match the Id in the request body.");

            var item = _mapper.Map<Item>(itemRequest);
            var updatedItem = await ItemHandler.UpdateAsync(item);
            if (updatedItem == null)
                return NotFound();

            return Ok(updatedItem);
        }

        /// <summary>
        /// Deletes an item by its ID.
        /// </summary>
        /// <param name="id">The ID of the item to delete.</param>
        /// <returns>An action result indicating success, or NotFound if the item is not found.</returns>
        // DELETE api/<ItemsController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await ItemHandler.GetByIdAsync(id);
            if (item == null)
                return NotFound();

            await ItemHandler.RemoveAsync(item);
            return NoContent();
        }
    }
}
