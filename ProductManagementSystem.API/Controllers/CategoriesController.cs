using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProductManagementSystem.Application.Categories;
using ProductManagementSystem.Shared.Dtos.Categories;
using ProductManagementSystem.Domain.Categories;
using Microsoft.AspNetCore.Authorization;

namespace ProductManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CategoriesController(CategoryHandler categoryHandler, IMapper _mapper) : ControllerBase
    {
        // GET: api/<CategoriesController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var categories = await categoryHandler.GetAllAsync();
            return Ok(categories);

        }

        // GET api/<CategoriesController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var category = await categoryHandler.GetByIdAsync(id);
            if (category == null)
                return NotFound();

            return Ok(category);
        }

        // POST api/<CategoriesController>
        [HttpPost]
        public async Task<IActionResult> Post(CategoryCreateRequest categoryRequest)
        {
            var category = _mapper.Map<Category>(categoryRequest);
            var addedCategory = await categoryHandler.AddAsync(category);
            return CreatedAtAction(nameof(Get), new { id = addedCategory.Id }, addedCategory);
        }

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
