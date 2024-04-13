using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProductManagementSystem.Application.Categories;
using ProductManagementSystem.Shared.Dtos.Categories;
using ProductManagementSystem.Domain.Categories;
using Microsoft.AspNetCore.Authorization;

namespace ProductManagementSystem.API.Controllers
{
    /// <summary>
    /// Controller for managing categories.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CategoriesController(CategoryHandler categoryHandler, IMapper _mapper) : ControllerBase
    {
        /// <summary>
        /// Retrieves all categories.
        /// </summary>
        /// <returns>An action result containing the list of categories.</returns>
        // GET: api/<CategoriesController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var categories = await categoryHandler.GetAllAsync();
            return Ok(categories);

        }

        /// <summary>
        /// Retrieves a category by its ID.
        /// </summary>
        /// <param name="id">The ID of the category to retrieve.</param>
        /// <returns>An action result containing the retrieved category, or NotFound if the category is not found.</returns>
        // GET api/<CategoriesController>/5

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var category = await categoryHandler.GetByIdAsync(id);
            if (category == null)
                return NotFound();

            return Ok(category);
        }

        /// <summary>
        /// Creates a new category.
        /// </summary>
        /// <param name="categoryRequest">The data for the new category.</param>
        /// <returns>An action result containing the created category.</returns>
        // POST api/<CategoriesController>
        [HttpPost]
        public async Task<IActionResult> Post(CategoryCreateRequest categoryRequest)
        {
            var category = _mapper.Map<Category>(categoryRequest);
            var addedCategory = await categoryHandler.AddAsync(category);
            return CreatedAtAction(nameof(Get), new { id = addedCategory.Id }, addedCategory);
        }

        /// <summary>
        /// Updates a category by its ID.
        /// </summary>
        /// <param name="id">The ID of the category to update.</param>
        /// <param name="categoryRequest">The updated data for the category.</param>
        /// <returns>An action result containing the updated category, or NotFound if the category is not found, or BadRequest if the provided ID does not match the ID in the request body.</returns>
        // PUT api/<CategoriesController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, CategoryUpdateRequest categoryRequest)
        {
            if (id != categoryRequest.Id)
                return BadRequest("The provided Id does not match the Id in the request body.");

            var category = _mapper.Map<Category>(categoryRequest);

            var updatedCategory = await categoryHandler.UpdateAsync(category);
            if (updatedCategory == null)
                return NotFound();

            return Ok(updatedCategory);
        }

        /// <summary>
        /// Deletes a category by its ID.
        /// </summary>
        /// <param name="id">The ID of the category to delete.</param>
        /// <returns>An action result indicating success, or NotFound if the category is not found.</returns>
        // DELETE api/<CategoriesController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var category = await categoryHandler.GetByIdAsync(id);
            if (category == null)
                return NotFound();

            await categoryHandler.RemoveAsync(category);
            return NoContent();
        }
    }
}
